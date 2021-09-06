using System;
using System.Collections.Generic;
using System.Linq;
using dotnet_restapi.Dtos;
using dotnet_restapi.Entities;
using dotnet_restapi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_restapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select(item => item.AsDto());
            return items;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto createItemDto)
        {
            Item item = new() {
                Id = Guid.NewGuid(),
                Name = createItemDto.Name,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            repository.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new { id = item.Id}, item.AsDto());
        }

        [HttpPatch("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = repository.GetItem(id);

            if (existingItem is null) {
                return BadRequest("Item does not exist.");
            }

            if (updateItemDto.Name is not null) {
                existingItem.Name = updateItemDto.Name;
            }

            if (updateItemDto.Price != 0) {
                existingItem.Price = updateItemDto.Price;
            }

            repository.UpdateItem(existingItem);

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateWholeItem(Guid id, UpdateWholeItemDto updateWholeItemDto)
        {
            var existingItem = repository.GetItem(id);

            if (existingItem is null) {
                return BadRequest("Item does not exist.");
            }

            var updatedItem = existingItem with {
                Name = updateWholeItemDto.Name,
                Price = updateWholeItemDto.Price
            };

            repository.UpdateItem(existingItem);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = repository.GetItem(id);

            if (existingItem is null) {
                return BadRequest("Item does not exist.");
            }

            repository.DeleteItem(id);

            return Ok();
        }
    }
}
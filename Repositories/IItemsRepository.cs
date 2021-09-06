using System;
using System.Collections.Generic;
using dotnet_restapi.Entities;

namespace dotnet_restapi.Repositories
{
    public interface IItemsRepository {
        public IEnumerable<Item> GetItems();

        public Item GetItem(Guid id);

        void CreateItem(Item item);

        void UpdateItem(Item item);

        void DeleteItem(Guid id);
    }
}
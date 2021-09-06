using System.ComponentModel.DataAnnotations;

namespace dotnet_restapi.Dtos
{
    public class UpdateItemDto
    {

        public string Name { get; set; }

        [Range(1, 10000000)]
        public decimal Price { get; set; }
    }
}
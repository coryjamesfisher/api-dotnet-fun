using System;

namespace dotnet_restapi.Dtos 
{

    public record ItemDto {
        
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
    }

}
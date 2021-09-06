using System.ComponentModel.DataAnnotations;

namespace dotnet_restapi.Dtos
{
    public class UpdateWholeItemDto
    {

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 10000000)]
        public decimal Price { get; set; }
    }
}
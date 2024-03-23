using System.ComponentModel.DataAnnotations;

namespace Services.BasketServices.Dto
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage ="Price must be above 0")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, 10) ]
        public int Quantity { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Brand { get; set; }

        public string Type { get; set; }
    }
}
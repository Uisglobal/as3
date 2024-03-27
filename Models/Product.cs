using System.ComponentModel.DataAnnotations;

namespace Assignment_3_APIs.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Image { get; set; }

        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }

        [Required]
        public decimal ShippingCost { get; set; }


    }
}
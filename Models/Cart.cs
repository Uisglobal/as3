using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_3_APIs.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Assignment_3_APIs.Models
{
    public class Order : OrderDetail
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCost { get; set; }
        public decimal Total { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
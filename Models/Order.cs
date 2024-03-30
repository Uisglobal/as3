namespace Assignment_3_APIs.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int orderUId { get; set; }

        public int orderProductID { get; set; }
        public int OrderQuantity { get; set; }
        public double total { get; set; }
    }
}

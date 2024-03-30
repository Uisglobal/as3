namespace Assignment_3_APIs.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int cartUId { get; set; }
        public int cartProdutID { get; set; }
        public int productQuantity { get; set; }
        public double cartTotalValue { get; set; }
    }
}

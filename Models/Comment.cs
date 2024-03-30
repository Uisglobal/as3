namespace Assignment_3_APIs.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int commentUId { get; set; }
        public int commentProductID { get; set; }
        public double ratings { get; set; }
        public string imgs { get; set; }
        public string text { get; set; }
    }
}

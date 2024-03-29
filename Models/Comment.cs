using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_3_APIs.Models
{

    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int Rating { get; set; }

        public string Image { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

    }

}
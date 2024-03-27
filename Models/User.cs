using System.ComponentModel.DataAnnotations;

namespace Assignment_3_APIs.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public string ShippingAddress { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace DemoAspNet.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Champs requis !")]
        public string? UserName { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}

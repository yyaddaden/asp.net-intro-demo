using System.ComponentModel.DataAnnotations;

namespace DemoAspNet.Models
{
    public class AddProduct
    {
        [Required(ErrorMessage = "Champs requis !")]
        public Product? Product { get; set; }
        [Required(ErrorMessage = "Champs requis !")]
        public int? UserId { get; set; }
        public ICollection<User>? Users { get; set;}
    }
}

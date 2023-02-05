using System.ComponentModel.DataAnnotations;

namespace DemoAspNet.Models
{
    public class Product
    {
        [Required(ErrorMessage = "Champs requis !")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Champs requis !")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Champs requis !")]
        public string? Country { get; set; }
    }
}

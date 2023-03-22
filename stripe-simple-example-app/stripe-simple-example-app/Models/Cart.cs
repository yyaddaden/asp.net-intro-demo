using System.ComponentModel.DataAnnotations;

namespace stripe_simple_example_app.Models
{
    public class Cart
    {
        [Required(ErrorMessage = "Champs requis !")]
        public string? Name { get; set; }
        public List<Item>? Items { get; set; }
    }
}
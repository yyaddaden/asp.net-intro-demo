using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace DemoAspNet.Models
{
    public class Product
    {
        [BindNever]
        [System.Text.Json.Serialization.JsonIgnore]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Champs requis !")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Champs requis !")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Champs requis !")]
        public string? Country { get; set; }
        public int UserId { get; set; }
        [BindNever]
        [System.Text.Json.Serialization.JsonIgnore]
        public User? User { get; set; }
    }
}

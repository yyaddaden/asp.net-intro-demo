using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace DemoAspNet.Models
{
    public class User
    {
        [BindNever]
        [System.Text.Json.Serialization.JsonIgnore]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Champs requis !")]
        public string? UserName { get; set; }
        [BindNever]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Product>? Products { get; set; }
    }
}

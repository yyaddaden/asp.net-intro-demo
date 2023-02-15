using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace temperature_converter_ef_core_app.Models
{
    public class User
    {
        public User()
        {
            Conversions = new List<Conversion>();
        }

        public int UserId { get; set; }
        [Required(ErrorMessage = "Champs Requis !")]
        public string? Name { get; set; }

        public ICollection<Conversion> Conversions { get; set; }
    }
}

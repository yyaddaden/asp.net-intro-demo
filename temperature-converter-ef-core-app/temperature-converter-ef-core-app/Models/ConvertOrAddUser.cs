using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace temperature_converter_ef_core_app.Models
{
    public class ConvertOrAddUser
    {
        public List<User> Users { get; set; }
        [Required(ErrorMessage = "Champs Requis !")]
        public Conversion? Conversion { get; set; }
        [Required(ErrorMessage = "Champs Requis !")]
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
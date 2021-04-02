using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace book_manager_app.Models
{
    public class Book
    {   
        [Required(ErrorMessage = "Champs requis !")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Champs requis !")]
        public int? NumberOfVolumes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace temperature_converter_app.Models
{
    public class Conversion
    {
        public char? InputMetric { get; set; }
        [Required(ErrorMessage = "Champs Requis !")]
        public float? InputValue { get; set; }
        [Required(ErrorMessage = "Champs Requis !")]
        public char? OutputMetric { get; set; }
        public float? OutputValue { get; set; }
    }
}

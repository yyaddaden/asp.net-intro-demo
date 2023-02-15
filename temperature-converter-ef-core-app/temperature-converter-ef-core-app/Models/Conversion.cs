using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;

namespace temperature_converter_ef_core_app.Models
{
    public class Conversion
    {
        public int ConversionId { get; set; }
        public char? InputMetric { get; set; }
        [Required(ErrorMessage = "Champs Requis !")]
        public float? InputValue { get; set; }
        [Required(ErrorMessage = "Champs Requis !")]
        public char? OutputMetric { get; set; }
        public float? OutputValue { get; set; }
        public DateTime DateTime { get; set; }

        public User? User { get; set; }

        [Required(ErrorMessage = "Champs Requis !")]
        public int? UserId { get; set; }
    }
}

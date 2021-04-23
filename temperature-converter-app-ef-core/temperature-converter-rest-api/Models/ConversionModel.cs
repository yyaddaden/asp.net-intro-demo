using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace temperature_converter_rest_api.Models
{
    public class ConversionModel
    {
        public char OutputMetric { get; set; }
        public float InputValue { get; set; }
    }
}

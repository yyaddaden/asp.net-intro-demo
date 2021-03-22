using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace temperature_converter_app.Controllers
{
    public class ConverterController : Controller
    {
        [HttpGet]
        public IActionResult Convert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Convert(Models.Conversion conversion)
        {
            if (ModelState.IsValid)
            {
                conversion.InputMetric = (conversion.OutputMetric == 'C') ? 'F' : 'C';

                switch (conversion.OutputMetric)
                {
                    case 'C':
                        conversion.OutputValue = (conversion.InputValue - 32) * ((float) 5 / 9);
                        break;
                    case 'F':
                        conversion.OutputValue = (conversion.InputValue * ((float) 9 / 5)) + 32;
                        break;
                }

                return RedirectToAction("Result", conversion);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Result(Models.Conversion conversion)
        {
            return View(conversion);
        }
    }
}

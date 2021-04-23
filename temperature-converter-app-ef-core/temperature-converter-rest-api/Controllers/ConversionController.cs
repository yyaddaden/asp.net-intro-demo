using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using temperature_converter_app_ef_core.Models;

namespace temperature_converter_rest_api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionController : ControllerBase
    {
        private TemperatureConverterDbContext _context;

        public ConversionController()
        {
            _context = new TemperatureConverterDbContext();
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<Conversion>), (int)HttpStatusCode.OK)]
        public IActionResult GetConversions(int userId)
        {
            try
            {
                User user = _context.Users.Include(u => u.Conversions).Where(u => u.UserId == userId).First();
                if (user != null)
                    return Ok(user.Conversions.ToList());
                else
                    return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception) { }

            return BadRequest();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Conversion), (int)HttpStatusCode.OK)]
        public IActionResult MakeConversion(int userId, Models.ConversionModel model)
        {
            try
            {
                User user = _context.Users.Include(u => u.Conversions).Where(u => u.UserId == userId).First();
                if (user != null)
                {
                    Conversion conversion = new Conversion()
                    {
                        OutputMetric = model.OutputMetric,
                        InputValue = model.InputValue,
                        InputMetric = (model.OutputMetric == 'C') ? 'F' : 'C',
                        UserId = userId
                    };

                    switch (conversion.OutputMetric)
                    {
                        case 'C':
                            conversion.OutputValue = (conversion.InputValue - 32) * ((float)5 / 9);
                            break;
                        case 'F':
                            conversion.OutputValue = (conversion.InputValue * ((float)9 / 5)) + 32;
                            break;
                    }

                    conversion.DateTime = DateTime.Now;
                    _context.Conversions.Add(conversion);
                    _context.SaveChanges();
                    return Ok(conversion);
                }
                else
                    return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception) { }

            return BadRequest();
        }
    }
}
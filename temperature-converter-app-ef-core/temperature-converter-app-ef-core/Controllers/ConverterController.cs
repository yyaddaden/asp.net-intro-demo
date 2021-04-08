using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace temperature_converter_app_ef_core.Controllers
{
    public class ConverterController : Controller
    {
        [HttpGet]
        public IActionResult ConvertOrAddUser()
        {
            Models.TemperatureConverterDbContext temperatureConverterDbContext = new Models.TemperatureConverterDbContext();
            Models.ConvertOrAddUser convertOrAddUser = new Models.ConvertOrAddUser() { Users = temperatureConverterDbContext.Users.ToList() };
            return View(convertOrAddUser);
        }

        [HttpPost]
        public IActionResult Convert(Models.Conversion conversion)
        {
            Models.TemperatureConverterDbContext temperatureConverterDbContext = new Models.TemperatureConverterDbContext();

            if (ModelState.IsValid)
            {
                conversion.InputMetric = (conversion.OutputMetric == 'C') ? 'F' : 'C';

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

                temperatureConverterDbContext.Conversions.Add(conversion);
                temperatureConverterDbContext.SaveChanges();

                return RedirectToAction("Result", conversion);
            }

            Models.ConvertOrAddUser convertOrAddUser = new Models.ConvertOrAddUser() { Users = temperatureConverterDbContext.Users.ToList() };

            return View("ConvertOrAddUser", convertOrAddUser);
        }

        [HttpPost]
        public IActionResult AddUser(Models.User user)
        {
            Models.TemperatureConverterDbContext temperatureConverterDbContext = new Models.TemperatureConverterDbContext();

            if (ModelState.IsValid)
            {
                temperatureConverterDbContext.Add(user);
                temperatureConverterDbContext.SaveChanges();
                return RedirectToAction("Create", "Converter", new { userId = user.UserId });
            }

            Models.ConvertOrAddUser convertOrAddUser = new Models.ConvertOrAddUser() { Users = temperatureConverterDbContext.Users.ToList() };

            return View("ConvertOrAddUser", convertOrAddUser);
        }

        [HttpGet]
        public IActionResult Create(int? userId = null)
        {
            Models.TemperatureConverterDbContext temperatureConverterDbContext = new Models.TemperatureConverterDbContext();

            if (userId == null)
            {
                Models.ConvertOrAddUser convertOrAddUser = new Models.ConvertOrAddUser() { Users = temperatureConverterDbContext.Users.ToList() };
                return View("ConvertOrAddUser", convertOrAddUser);
            }

            Models.User user = temperatureConverterDbContext.Users.Find(userId);

            return View(user);
        }

        [HttpGet]
        public IActionResult Result(Models.Conversion conversion = null)
        {
            if (!ModelState.IsValid)
            {
                Models.TemperatureConverterDbContext temperatureConverterDbContext = new Models.TemperatureConverterDbContext();
                Models.ConvertOrAddUser convertOrAddUser = new Models.ConvertOrAddUser() { Users = temperatureConverterDbContext.Users.ToList() };

                return View("ConvertOrAddUser", convertOrAddUser);
            }

            return View(conversion);
        }

        [HttpPost]
        public IActionResult History(Models.User user)
        {
            if(user.UserId != 0)
                return RedirectToAction("History", "Converter", new { userId = user.UserId });

            return RedirectToAction("ConvertOrAddUser");
        }

        [HttpGet]
        public IActionResult History(int? userId = null)
        {
            Models.TemperatureConverterDbContext temperatureConverterDbContext = new Models.TemperatureConverterDbContext();

            if (userId == null)
            {
                Models.ConvertOrAddUser convertOrAddUser = new Models.ConvertOrAddUser() { Users = temperatureConverterDbContext.Users.ToList() };
                return View("ConvertOrAddUser", convertOrAddUser);
            }

            List<Models.Conversion> conversions = temperatureConverterDbContext.Conversions.Where(c => c.UserId == userId).ToList();
            Models.User user = temperatureConverterDbContext.Users.Find(userId);

            return View(new Tuple<Models.User, List<Models.Conversion>>(user, conversions));
        }
    }
}

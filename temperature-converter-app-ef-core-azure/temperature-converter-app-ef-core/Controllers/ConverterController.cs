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
        private Models.TemperatureConverterDbContext _context;

        public ConverterController(Models.TemperatureConverterDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ConvertOrAddUser()
        {
            Models.ConvertOrAddUser convertOrAddUser = new Models.ConvertOrAddUser() { Users = _context.Users.ToList() };
            return View(convertOrAddUser);
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
                        conversion.OutputValue = (conversion.InputValue - 32) * ((float)5 / 9);
                        break;
                    case 'F':
                        conversion.OutputValue = (conversion.InputValue * ((float)9 / 5)) + 32;
                        break;
                }

                conversion.DateTime = DateTime.Now;

                _context.Conversions.Add(conversion);
                _context.SaveChanges();

                return RedirectToAction("Result", conversion);
            }

            Models.ConvertOrAddUser convertOrAddUser = new Models.ConvertOrAddUser() { Users = _context.Users.ToList() };

            return View("ConvertOrAddUser", convertOrAddUser);
        }

        [HttpPost]
        public IActionResult AddUser(Models.User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Create", "Converter", new { userId = user.UserId });
            }

            Models.ConvertOrAddUser convertOrAddUser = new Models.ConvertOrAddUser() { Users = _context.Users.ToList() };

            return View("ConvertOrAddUser", convertOrAddUser);
        }

        [HttpGet]
        public IActionResult Create(int? userId = null)
        {
            if (userId == null)
            {
                Models.ConvertOrAddUser convertOrAddUser = new Models.ConvertOrAddUser() { Users = _context.Users.ToList() };
                return View("ConvertOrAddUser", convertOrAddUser);
            }

            Models.User user = _context.Users.Find(userId);

            return View(user);
        }

        [HttpGet]
        public IActionResult Result(Models.Conversion conversion = null)
        {
            if (!ModelState.IsValid)
            {
                Models.ConvertOrAddUser convertOrAddUser = new Models.ConvertOrAddUser() { Users = _context.Users.ToList() };

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
            if (userId == null)
            {
                Models.ConvertOrAddUser convertOrAddUser = new Models.ConvertOrAddUser() { Users = _context.Users.ToList() };
                return View("ConvertOrAddUser", convertOrAddUser);
            }

            List<Models.Conversion> conversions = _context.Conversions.Where(c => c.UserId == userId).ToList();
            Models.User user = _context.Users.Find(userId);

            return View(new Tuple<Models.User, List<Models.Conversion>>(user, conversions));
        }
    }
}

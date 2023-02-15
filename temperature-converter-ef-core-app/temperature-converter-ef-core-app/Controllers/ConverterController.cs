using Microsoft.AspNetCore.Mvc;

namespace temperature_converter_ef_core_app.Controllers
{
    public class ConverterController : Controller
    {
        private readonly TemperatureConverterDbContext _context;

        public ConverterController(TemperatureConverterDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult ConvertOrAddUser(Models.ConvertOrAddUser convertOrAddUser)
        {
            convertOrAddUser.Users = this._context.Users.ToList();
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

                this._context.Conversions.Add(conversion);
                this._context.SaveChanges();

                return RedirectToAction("Result", conversion);
            }

            Models.ConvertOrAddUser convertOrAddUser = new Models.ConvertOrAddUser() { Users = this._context.Users.ToList() };
            return View("ConvertOrAddUser", convertOrAddUser);
        }

        [HttpPost]
        public IActionResult AddUser(Models.User user)
        {
            if (ModelState.IsValid)
            {
                this._context.Add(user);
                this._context.SaveChanges();
                return RedirectToAction("Create", "Converter", new { userId = user.UserId });
            }

            Models.ConvertOrAddUser convertOrAddUser = new Models.ConvertOrAddUser() { Users = this._context.Users.ToList() };

            return View("ConvertOrAddUser", convertOrAddUser);
        }

        [HttpGet]
        public IActionResult Create(int? userId = null)
        {
            if (userId == null)
            {
                Models.ConvertOrAddUser convertOrAddUser = new Models.ConvertOrAddUser() { Users = this._context.Users.ToList() };
                return View("ConvertOrAddUser", convertOrAddUser);
            }

            Models.User user = this._context.Users.Find(userId);

            return View(user);
        }

        [HttpGet]
        public IActionResult Result(Models.Conversion conversion = null)
        {
            if (!ModelState.IsValid)
            {
                Models.ConvertOrAddUser convertOrAddUser = new Models.ConvertOrAddUser() { Users = this._context.Users.ToList() };

                return View("ConvertOrAddUser", convertOrAddUser);
            }

            return View(conversion);
        }

        [HttpPost]
        public IActionResult History(int userId)
        {
            if (ModelState.IsValid)
                return RedirectToAction("ListHistory", new { userId = userId });

            return RedirectToAction("ConvertOrAddUser");
        }

        [HttpGet]
        [Route("Converter/History/{userId:int}")]
        public IActionResult ListHistory(int userId)
        {
            if (userId == null)
            {
                Models.ConvertOrAddUser convertOrAddUser = new Models.ConvertOrAddUser() { Users = this._context.Users.ToList() };
                return View("ConvertOrAddUser", convertOrAddUser);
            }

            List<Models.Conversion> conversions = this._context.Conversions.Where(c => c.UserId == userId).ToList();
            Models.User user = this._context.Users.Find(userId);

            return View(new Tuple<Models.User, List<Models.Conversion>>(user, conversions));
        }
    }
}

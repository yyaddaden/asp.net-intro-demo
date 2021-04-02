using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_manager_app.Controllers
{
    public class BookController : Controller
    {   
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Models.Book book)
        {
            if (ModelState.IsValid)
                return RedirectToAction("Result", book);

            return View();
        }

        [HttpGet]
        public IActionResult Result(Models.Book book)
        {
            
            List<Models.Book> books = new List<Models.Book>()
            {
                new Models.Book() { Title = "Harry Potter", NumberOfVolumes = 7 },
                new Models.Book() { Title = "Lord of the Rings", NumberOfVolumes = 3 },
            };

            books.Add(book);

            return View(books);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace seance_04_exercice_01.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Result(Models.Book user_book)
        {
            List<Models.Book> books = new List<Models.Book>() {
                new Models.Book() { Title = "Han d'Islande", Author = "Victor Hugo", Editor = "Plume de Carotte", ImageCover = "han-dislande.jpg" },
                new Models.Book() { Title = "Le dernier Jour d’un Condamné", Author = "Victor Hugo", Editor = "Arvensa", ImageCover = "le-dernier-jour-dun-condamné.jpg" },
                new Models.Book() { Title = "Le Silmarillion", Author = "J.R.R. Tolkien", Editor = "Bourgois", ImageCover = "le-silmarillon.jpg" },
                new Models.Book() { Title = "Le Seigneur des anneaux : Les Deux Tours", Author = "J.R.R. Tolkien", Editor = "Bourgois", ImageCover = "le-seigneur-des-anneaux-ii.jpg" },
                new Models.Book() { Title = "Le Seigneur des anneaux : Le Retour du roi", Author = "J.R.R. Tolkien", Editor = "Bourgois", ImageCover = "le-seigneur-des-anneaux-iii.jpg" },
                new Models.Book() { Title = "Le Portrait de Dorian Gray", Author = "Oscar Wilde", Editor = "Le livre qui parle", ImageCover = "le-portrait-de-dorian-gray.jpg" }
            };

            List<Models.Book> match_books = new List<Models.Book>();

            user_book.Title = String.IsNullOrEmpty(user_book.Title) ? String.Empty : user_book.Title;
            user_book.Author = String.IsNullOrEmpty(user_book.Author) ? String.Empty : user_book.Author;
            user_book.Editor = String.IsNullOrEmpty(user_book.Editor) ? String.Empty : user_book.Editor;

            foreach (Models.Book book in books)
            {
                if (book.Title.Contains(user_book.Title) && book.Author.Contains(user_book.Author) && book.Editor.Contains(user_book.Editor))
                {
                    match_books.Add(book);
                }
            }

            return View(match_books);
        }
    }
}

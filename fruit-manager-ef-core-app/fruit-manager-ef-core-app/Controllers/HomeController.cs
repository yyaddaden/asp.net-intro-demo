using DemoAspNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoAspNet.Controllers
{
    public class HomeController : Controller
    {
        public DemoAspNetDbContext DbContext { get; set; }

        public HomeController()
        {
            this.DbContext = new DemoAspNetDbContext();
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Page des produits";
            List<Models.Product> products = this.DbContext.Products.Include(p => p.User).ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.Title = "Ajouter un produit";
            Models.AddProduct addProduct = new AddProduct();
            addProduct.Users = this.DbContext.Users.ToList();
            return View(addProduct);
        }

        [HttpPost]
        public IActionResult AddProduct(Models.AddProduct _addProduct)
        {
            ViewBag.Title = "Ajouter un produit";

            if (ModelState.IsValid)
            {
                Models.User user = this.DbContext.Users.Include(u => u.Products).Where(u => u.UserId == _addProduct.UserId).First();
                user.Products.Add(_addProduct.Product);
                this.DbContext.SaveChanges();
                return RedirectToAction("ProductResult", new { productId = _addProduct.Product.ProductId });
            }

            Models.AddProduct addProduct = new AddProduct();
            addProduct.Users = this.DbContext.Users.ToList();
            return View(addProduct);
        }

        [HttpGet]
        public IActionResult ProductResult(int productId)
        {
            ViewBag.Title = "Page de résultat";
            Models.Product product = this.DbContext.Products.Include(p => p.User).Where(p => p.ProductId == productId).First();
            return View(product);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            ViewBag.Title = "Ajouter un utilisateur";
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(Models.User _user)
        {
            ViewBag.Title = "Ajouter un utilisateur";

            if (ModelState.IsValid)
            {
                this.DbContext.Users.Add(_user);
                this.DbContext.SaveChanges();
                return RedirectToAction("UserResult", _user);
            }
              
            return View();
        }

        [HttpGet]
        public IActionResult UserResult(Models.User user)
        {
            ViewBag.Title = "Page de résultat";
            return View(user);
        }

        public IActionResult Contact()
        {
            ViewBag.Title = "Page de contact";
            return View();
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("Home/RemoveProduct/{productId:int}")]
        public IActionResult RemoveProduct(int productId)
        {
            Models.Product product = this.DbContext.Products.Find(productId);
            this.DbContext.Remove(product);
            this.DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

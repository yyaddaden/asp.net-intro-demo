using Microsoft.AspNetCore.Mvc;
using System.Net;

using DemoAspNet;
using DemoAspNet.Models;

namespace fruit_manager_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitController : ControllerBase
    {
        private DemoAspNetDbContext _context;
        public FruitController()
        {
            _context = new DemoAspNetDbContext();
        }

        [HttpGet(Name = "GetProducts")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetProducts()
        {
            try
            {
                List<Product> products = _context.Products.ToList();
                return Ok(products);
            }
            catch (Exception)
            {
                return BadRequest("Une erreur est surevenu lors du traitement de la requête !");
            }
        }

        [HttpGet("{productId}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetProduct(int productId)
        {
            try
            {
                Product product = _context.Products.Find(productId);
                if(product is not null)
                    return Ok(product);
                else
                    return NotFound($"Le produit avec l'Id ({productId}) fourni n'existe pas !");
            }
            catch (Exception)
            {
                return BadRequest("Une erreur est surevenu lors du traitement de la requête !");
            }
        }

        [HttpPost(Name = "AddProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult AddProduct([FromBody] Product product)
        {
            try
            {
                User user = _context.Users.Find(product.UserId);

                if (user is not null)
                {
                    Product productToAdd = new Product()
                    {
                        Title = product.Title,
                        Description = product.Description,
                        Country = product.Country,
                        User = user
                    };
                    _context.Products.Add(productToAdd);
                    _context.SaveChanges();
                    return Created($"{Request.Host}{Request.PathBase}{Request.Path}{Request.QueryString}/{productToAdd.ProductId}", productToAdd);
                }
                else
                    return NotFound($"L'utilisateur avec l'Id ({product.UserId}) fourni n'existe pas !");
            }
            catch (Exception)
            {
                return BadRequest("Une erreur est surevenu lors du traitement de la requête !");
            }
        }

        [HttpDelete("{productId}", Name = "RemoveProduct")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult RemoveProduct(int productId)
        {
            try
            {
                Product product = _context.Products.Find(productId);
                if (product is not null)
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                    return Ok($"Le produit avec l'Id {productId} a été supprimé avec succès !");
                }
                else
                    return NotFound($"Le produit avec l'Id ({productId}) fourni n'existe pas !");
            }
            catch (Exception)
            {
                return BadRequest("Une erreur est surevenu lors du traitement de la requête !");
            }
        }
    }
}

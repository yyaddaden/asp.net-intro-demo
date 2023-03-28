using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;

using DemoAspNet;
using DemoAspNet.Models;

namespace fruit_manager_api.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DemoAspNetDbContext _context;
        public UserController() 
        {
            _context = new DemoAspNetDbContext();
        }

        [HttpGet(Name = "GetUsers")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetUsers()
        {
            try
            {
                List<User> users = _context.Users.Include(u => u.Products).ToList();
                return Ok(users);
            }
            catch (Exception)
            {
                return BadRequest("Une erreur est surevenu lors du traitement de la requête !");
            }
        }

        [HttpGet("{userId}", Name = "GetUser")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetUser(int userId)
        {
            try
            {
                User user = _context.Users.Find(userId);
                if (user is not null)
                    return Ok(user);
                else
                    return NotFound($"L'utilisateur avec l'Id ({userId}) fourni n'existe pas !");
            }
            catch (Exception)
            {
                return BadRequest("Une erreur est surevenu lors du traitement de la requête !");
            }
        }

        [HttpPost(Name = "AddUser")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult AddUser([FromForm] User user)
        {
            try
            {
                User userToAdd = new User() { UserName = user.UserName };
                _context.Users.Add(userToAdd);
                _context.SaveChanges();
                return Created($"{Request.Host}{Request.PathBase}{Request.Path}{Request.QueryString}/{userToAdd.UserId}", userToAdd);
            }
            catch (Exception) 
            {
                return BadRequest("Une erreur est surevenu lors du traitement de la requête !");
            }
        }
    }
}

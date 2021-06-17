using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using temperature_converter_app_ef_core.Models;

namespace temperature_converter_rest_api.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private TemperatureConverterDbContext _context;

        public UserController(TemperatureConverterDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(List<User>), (int)HttpStatusCode.OK)]
        public IActionResult GetUsers()
        {
            try
            {
                List<User> users = _context.Users.ToList();
                return Ok(users);
            }
            catch (Exception) { }

            return BadRequest();
        }

        [HttpGet("{userId}", Name = "GetUser")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public IActionResult GetUser(int userId)
        {
            try
            {
                User user = _context.Users.Find(userId);
                if (user != null)
                    return Ok(user);
                else
                    return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception) { }

            return BadRequest();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult AddUser([FromBody] Models.UserModel model)
        {
            try
            {
                User user = new User()
                {
                    Name = model.Name,
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetUser), new { userId = user.UserId }, user);

            }
            catch (Exception) { }

            return BadRequest();
        }
    }
}

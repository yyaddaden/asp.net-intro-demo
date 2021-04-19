using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace task_manager_rest_api.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private Models.TaskManagerDbContext _context;

        public UserController()
        {
            _context = new Models.TaskManagerDbContext();
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetUsers()
        {
            try
            {
                List<Models.User> users = _context.Users.ToList();
                return Ok(users);
            }
            catch (Exception) { }

            return BadRequest();
        }

        [HttpGet("{userId}", Name = "GetUser")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetUser(int userId)
        {
            try
            {
                Models.User user = _context.Users.Find(userId);
                if (user != null)
                    return Ok(user);
                else
                    return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception) { }

            return BadRequest();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Models.UserModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult AddUser([FromBody] Models.UserModel model)
        {
            try
            {
                Models.User user = new Models.User()
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

        [HttpDelete("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult RemoveUser(int userId)
        {
            try
            {
                Models.User user = _context.Users.Find(userId);
                if (user != null)
                {
                    _context.Remove(user);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                    return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception) { }

            return BadRequest();
        }
    }
}

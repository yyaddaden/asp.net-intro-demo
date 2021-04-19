using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;

namespace task_manager_rest_api.Controllers
{
    [Produces("application/json")]
    [Route("api/Tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private Models.TaskManagerDbContext _context;

        public TaskController()
        {
            _context = new Models.TaskManagerDbContext();
        }

        [HttpGet("{taskId}", Name = "GetTask")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetTask(int taskId)
        {
            try
            {
                Models.Task task = _context.Tasks.Find(taskId);
                if(task != null)
                    return Ok(task);
                else
                    return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception) { }

            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetTasks(int userId)
        {
            try 
            {
                Models.User user = _context.Users.Include(u => u.Tasks).ThenInclude(t => t.Priority).Where(u => u.UserId == userId).First();
                if (user != null)
                    return Ok(user.Tasks.ToList());
                else
                    return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception) { }

            return BadRequest();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Models.TaskModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult AddTask([FromForm] Models.TaskModel model)
        {
            try
            {
                Models.Task task = new Models.Task()
                {
                    Title = model.Title,
                    Status = model.Status,
                    DueDate = model.DueDate,
                    UserId = model.UserId,
                    PriorityId = model.PriorityId
                };
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetTask), new { taskId = task.TaskId }, task);
            }
            catch (Exception) { }

            return BadRequest();
        }

        [HttpDelete("{taskId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult RemoveTask(int taskId)
        {
            try
            {
                Models.Task task = _context.Tasks.Find(taskId);
                if (task != null)
                {
                    _context.Remove(task);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                    return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception) { }

            return BadRequest();
        }

        [HttpPatch("Complete/{taskId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult CompleteTask(int taskId)
        {
            try
            {
                Models.Task task = _context.Tasks.Find(taskId);
                if (task != null)
                {
                    task.Status = !task.Status;
                    _context.SaveChanges();
                    return Ok();
                }
                else
                    return StatusCode((int)HttpStatusCode.NotFound);
            }
            catch (Exception) { }

            return BadRequest();
        }

        [HttpPut("{taskId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult EditTask(int taskId, [FromForm] Models.TaskModel model)
        {
            try
            {
                Models.Task task = _context.Tasks.Find(taskId);
                if (task != null)
                {
                    task.Title = model.Title ?? task.Title;
                    task.DueDate = model.DueDate ?? task.DueDate;
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

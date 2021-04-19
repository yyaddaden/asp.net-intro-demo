using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task_manager_rest_api.Models
{
    public class User
    {
        public User()
        {
            Tasks = new List<Task>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}

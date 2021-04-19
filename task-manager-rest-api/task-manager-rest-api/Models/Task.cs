using System;

namespace task_manager_rest_api.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }
        public DateTime? DueDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PriorityId { get; set; }
        public Priority Priority { get; set; }
    }
}

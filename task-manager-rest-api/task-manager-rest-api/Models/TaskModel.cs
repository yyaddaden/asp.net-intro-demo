using System;

namespace task_manager_rest_api.Models
{
    public class TaskModel
    {
        public string Title { get; set; }
        public bool Status { get; set; }
        public DateTime? DueDate { get; set; }

        public int UserId { get; set; }
        public int PriorityId { get; set; }
    }
}

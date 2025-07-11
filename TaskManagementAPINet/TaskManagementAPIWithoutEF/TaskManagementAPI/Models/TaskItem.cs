// Models/TaskItem.cs

using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? TaskDueDate { get; set; }

        public int? TaskPriority { get; set; }

        public int? UserId { get; set; }

        // ✅ New field matching task_status_id from DB
        public int? TaskStatusId { get; set; }
    }
}
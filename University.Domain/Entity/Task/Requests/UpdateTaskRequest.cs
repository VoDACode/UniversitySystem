using System.ComponentModel.DataAnnotations;
using University.Domain.Entity.User;

namespace University.Domain.Entity.Task.Requests
{
    public class UpdateTaskRequest
    {
        [Required]
        [StringLength(128)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(2048)]
        public string Content { get; set; } = null!;
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        public int TeacherId { get; set; }
        public UserEntity Teacher { get; set; } = null!;
        public DateTime? Deadline { get; set; } = null;
        [Required]
        [Range(0, int.MaxValue)]
        public int MaxScore { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int MaxFiles { get; set; }
        [Required]
        public bool CanUpdate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace University.Domain.Entity.Task.Requests
{
    public class CreateTaskRequest
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
        public DateTime? Deadline { get; set; } = null;
        [Range(0, int.MaxValue)]
        public int MaxScore { get; set; } = 0;
        [Range(0, int.MaxValue)]
        public int MaxFiles { get; set; } = 0;
        [Required]
        public bool CanUpdate { get; set; }
    }
}

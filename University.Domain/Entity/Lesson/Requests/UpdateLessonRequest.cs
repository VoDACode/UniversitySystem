using System.ComponentModel.DataAnnotations;

namespace University.Domain.Entity.Lesson.Requests
{
    public class UpdateLessonRequest
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; } = null!;
        [StringLength(1024)]
        public string? Description { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
    }
}

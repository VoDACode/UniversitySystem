using System.ComponentModel.DataAnnotations;
using University.Domain.Entity.Course;
using University.Domain.Entity.Group;
using University.Domain.Entity.User;

namespace University.Domain.Entity.Lesson
{
    public class LessonEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; } = null!;
        [StringLength(1024)]
        public string? Description { get; set; }
        [Required]
        public int GroupId { get; set; }
        public GroupEntity Group { get; set; } = null!;
        [Required]
        public int CourseId { get; set; }
        public CourseEntity Course { get; set; } = null!;
        [Required]
        public int TeacherId { get; set; }
        public UserEntity Teacher { get; set; } = null!;
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
    }
}

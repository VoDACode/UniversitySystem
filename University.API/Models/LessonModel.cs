using System.ComponentModel.DataAnnotations;

namespace University.API.Models
{
    public class LessonModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int GroupId { get; set; }
        public GroupModel Group { get; set; } = null!;
        [Required]
        public int CourseId { get; set; }
        public CourseModel Course { get; set; } = null!;
        [Required]
        public int TeacherId { get; set; }
        public UserModel Teacher { get; set; } = null!;
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
    }
}

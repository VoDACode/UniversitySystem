using System.ComponentModel.DataAnnotations;

namespace University.API.Models
{
    public class CourseModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; } = null!;
        [Required]
        public DateOnly CreateAt { get; set; }

        public ICollection<GroupModel> Groups { get; set; } = new List<GroupModel>();
        public ICollection<LessonModel> Lessons { get; set; } = new List<LessonModel>();
        public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
    }
}

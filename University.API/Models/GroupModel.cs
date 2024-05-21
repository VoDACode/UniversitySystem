using System.ComponentModel.DataAnnotations;

namespace University.API.Models
{
    public class GroupModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(32)]
        public string Name { get; set; } = null!;
        [Required]
        public int TeacherId { get; set; }
        public UserModel Teacher { get; set; } = null!;
        [Required]
        public bool IsSubGroup { get; set; }
        public ICollection<UserModel> Students { get; set; } = new List<UserModel>();
        public ICollection<LessonModel> Lessons { get; set; } = new List<LessonModel>();
        public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
        public ICollection<CourseModel> Courses { get; set; } = new List<CourseModel>();
    }
}

using System.ComponentModel.DataAnnotations;
using University.Domain.Entity.Course;
using University.Domain.Entity.Lesson;
using University.Domain.Entity.Task;
using University.Domain.Entity.User;

namespace University.Domain.Entity.Group
{
    public class GroupEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(32)]
        public string Name { get; set; } = null!;
        [Required]
        public int TeacherId { get; set; }
        public UserEntity Teacher { get; set; } = null!;
        [Required]
        public bool IsSubGroup { get; set; }
        public ICollection<UserEntity> Students { get; set; } = new List<UserEntity>();
        public ICollection<LessonEntity> Lessons { get; set; } = new List<LessonEntity>();
        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
        public ICollection<CourseEntity> Courses { get; set; } = new List<CourseEntity>();
    }
}

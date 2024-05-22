using System.ComponentModel.DataAnnotations;
using University.Domain.Entity.Group;
using University.Domain.Entity.Lesson;
using University.Domain.Entity.Task;

namespace University.Domain.Entity.Course
{
    public class CourseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; } = null!;
        [Required]
        public DateOnly CreateAt { get; set; }

        public ICollection<GroupEntity> Groups { get; set; } = new List<GroupEntity>();
        public ICollection<LessonEntity> Lessons { get; set; } = new List<LessonEntity>();
        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}

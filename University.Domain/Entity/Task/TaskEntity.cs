using System.ComponentModel.DataAnnotations;
using University.Domain.Entity.Course;
using University.Domain.Entity.Group;
using University.Domain.Entity.TaskAnswer;
using University.Domain.Entity.User;

namespace University.Domain.Entity.Task
{
    public class TaskEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(2048)]
        public string Content { get; set; } = null!;
        [Required]
        public int CourseId { get; set; }
        public CourseEntity Course { get; set; } = null!;
        [Required]
        public int GroupId { get; set; }
        public GroupEntity Group { get; set; } = null!;
        [Required]
        public int TeacherId { get; set; }
        public UserEntity Teacher { get; set; } = null!;
        public DateTime? Deadline { get; set; } = null;
        public int MaxMark { get; set; } = 0;
        public int MaxFiles { get; set; } = 0;
        public bool CanUpdate { get; set; } = false;

        public ICollection<TaskAnswerEntity> TaskAnswers { get; set; } = new List<TaskAnswerEntity>();
    }
}

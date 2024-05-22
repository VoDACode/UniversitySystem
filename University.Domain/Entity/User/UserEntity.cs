using System.ComponentModel.DataAnnotations;
using University.Domain.Entity.File;
using University.Domain.Entity.Group;
using University.Domain.Entity.Lesson;
using University.Domain.Entity.Task;
using University.Domain.Entity.TaskAnswer;

namespace University.Domain.Entity.User
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(64)]
        public string LastName { get; set; } = null!;
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public long TaxId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [Phone]
        public string Phone { get; set; } = null!;
        [Required]
        public UserRole Role { get; set; }

        public ICollection<GroupEntity> Groups { get; set; } = new List<GroupEntity>();
        public ICollection<GroupEntity> TeachGroups { get; set; } = new List<GroupEntity>();
        public ICollection<TaskAnswerEntity> TaskAnswers { get; set; } = new List<TaskAnswerEntity>();
        public ICollection<TaskAnswerEntity> CheckedTaskAnswers { get; set; } = new List<TaskAnswerEntity>();
        public ICollection<LessonEntity> Lessons { get; set; } = new List<LessonEntity>();
        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
        public ICollection<FileEntity> Files { get; set; } = new List<FileEntity>();
    }
}

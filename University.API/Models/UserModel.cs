using System.ComponentModel.DataAnnotations;

namespace University.API.Models
{
    public class UserModel
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

        public ICollection<GroupModel> Groups { get; set; } = new List<GroupModel>();
        public ICollection<GroupModel> TeachGroups { get; set; } = new List<GroupModel>();
        public ICollection<TaskAnswerModel> TaskAnswers { get; set; } = new List<TaskAnswerModel>();
        public ICollection<LessonModel> Lessons { get; set; } = new List<LessonModel>();
        public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
        public ICollection<FileModel> Files { get; set; } = new List<FileModel>();
    }
}

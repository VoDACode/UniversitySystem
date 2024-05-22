using System.ComponentModel.DataAnnotations;
using University.Domain.Entity.File;
using University.Domain.Entity.Task;
using University.Domain.Entity.User;

namespace University.Domain.Entity.TaskAnswer
{
    public class TaskAnswerEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TaskId { get; set; }
        public TaskEntity Task { get; set; } = null!;
        [Required]
        public int StudentId { get; set; }
        public UserEntity Student { get; set; } = null!;
        public int? Mark { get; set; } = null;
        public DateTime? MarkedAt { get; set; } = null;
        public int? TeacherId { get; set; } = null;
        public UserEntity? Teacher { get; set; } = null; 
        public ICollection<FileEntity> Files { get; set; } = new List<FileEntity>();
    }
}

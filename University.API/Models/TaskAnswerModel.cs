using System.ComponentModel.DataAnnotations;

namespace University.API.Models
{
    public class TaskAnswerModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TaskId { get; set; }
        public TaskModel Task { get; set; } = null!;
        [Required]
        public int StudentId { get; set; }
        public UserModel Student { get; set; } = null!;
        public int Mark { get; set; }
        public DateTime? MarkedAt { get; set; }
        public int? TeacherId { get; set; }
        public UserModel? Teacher { get; set; }
        public ICollection<FileModel> Files { get; set; } = new List<FileModel>();
    }
}

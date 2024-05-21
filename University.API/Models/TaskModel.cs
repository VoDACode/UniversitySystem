using System.ComponentModel.DataAnnotations;

namespace University.API.Models
{
    public class TaskModel
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
        public CourseModel Course { get; set; } = null!;
        [Required]
        public int GroupId { get; set; }
        public GroupModel Group { get; set; } = null!;
        [Required]
        public int TeacherId { get; set; }
        public UserModel Teacher { get; set; } = null!;
        public DateTime? Deadline { get; set; } = null;
        public int MaxScore { get; set; } = 0;
        public int MaxFiles { get; set; } = 0;

        public ICollection<TaskAnswerModel> TaskAnswers { get; set; } = new List<TaskAnswerModel>();
    }
}

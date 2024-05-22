using System.ComponentModel.DataAnnotations;

namespace University.Domain.Entity.TaskAnswer.Requests
{
    public class EvaluateTaskAnswerRequest
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Mark { get; set; }
        [Required]
        [MaxLength(512)]
        public string Feedback { get; set; } = null!;
        public int TeacherId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using University.Domain.Entity.TaskAnswer;
using University.Domain.Entity.User;

namespace University.Domain.Entity.File
{
    public class FileEntity
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public long Size { get; set; } = 0;
        [Required]
        public int OwnerId { get; set; }
        public UserEntity Owner { get; set; } = null!;

        public int? TaskAnswerId { get; set; }
        public TaskAnswerEntity? TaskAnswer { get; set; }
    }
}

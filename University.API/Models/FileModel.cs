using System.ComponentModel.DataAnnotations;

namespace University.API.Models
{
    public class FileModel
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public long Size { get; set; } = 0;
        [Required]
        public int OwnerId { get; set; }
        public UserModel Owner { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace University.Domain.Entity.Group.Requests
{
    public class UpdateGroupRequest
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; } = null!;
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public bool IsSubGroup { get; set; }
    }
}

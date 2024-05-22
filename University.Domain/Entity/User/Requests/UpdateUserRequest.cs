using System.ComponentModel.DataAnnotations;

namespace University.Domain.Entity.User.Requests
{
    public class UpdateUserRequest
    {
        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(64)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public long TaxId { get; set; }
        [MaxLength(64)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public UserRole Role { get; set; }
    }
}

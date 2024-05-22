using System.ComponentModel.DataAnnotations;

namespace University.Domain.Entity.User.Requests
{
    public class CreateUserRequest
    {
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
    }
}

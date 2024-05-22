using System.ComponentModel.DataAnnotations;

namespace University.Domain.Entity.User.Requests
{
    public class UpdateUserRequest
    {
        [MaxLength(64)]
        public string? FirstName { get; set; }
        [MaxLength(64)]
        public string? LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public long TaxId { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? Phone { get; set; }
        public UserRole? Role { get; set; } = null;
    }
}

using University.Domain.Entity.User;

namespace University.Domain.Entity.User.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string RoleName { get; set; } = null!;

        public UserResponse(UserEntity user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            RoleName = user.Role.ToString();
        }

        public static implicit operator UserResponse(UserEntity user)
        {
            return new UserResponse(user);
        }
    }
}

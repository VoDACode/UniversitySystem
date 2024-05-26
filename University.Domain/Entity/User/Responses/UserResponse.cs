using University.Domain.Entity.User;

namespace University.Domain.Entity.User.Responses
{
    public class UserResponse
    {
        public int Id { get; set; } = 0;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;

        public UserResponse(UserEntity user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            RoleName = user.Role.ToString();
        }

        public UserResponse()
        {
        }

        public static implicit operator UserResponse(UserEntity user)
        {
            return new UserResponse(user);
        }
    }
}

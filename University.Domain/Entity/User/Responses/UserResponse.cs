namespace University.Domain.Entity.User.Responses
{
    public class UserResponse
    {
        public int Id { get; set; } = 0;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public string FullName => $"{FirstName} {LastName}";
        public string ShortName => $"{FirstName[0]}. {LastName}";

        public UserResponse(UserEntity user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            IsDeleted = user.IsDeleted;
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

using University.Domain.Entity.User;

namespace University.Domain.Entity.User.Responses
{
    public class DetailedUserResponse : UserResponse
    {
        public string Phone { get; set; } = null!;
        public long TaxId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DetailedUserResponse(UserEntity user) : base(user)
        {
            Phone = user.Phone;
            TaxId = user.TaxId;
            DateOfBirth = user.DateOfBirth.ToDateTime(TimeOnly.MinValue);
        }

        public static implicit operator DetailedUserResponse(UserEntity user)
        {
            return new DetailedUserResponse(user);
        }
    }
}

using University.Domain.Entity.User.Requests;
using University.Domain.Entity.User.Responses;
using University.Domain.Requests;
using University.Domain.Responses;

namespace University.Domain.Services
{
    public interface IUserService
    {
        Task<UserResponse> GetUserById(int id);
        Task<PageResponse<UserResponse>> GetUsers(PageRequest request);
        Task<DetailedUserResponse> GetDetailedUserById(int id);
        Task<UserResponse> CreateUser(CreateUserRequest request);
        Task<UserResponse> UpdateUser(int id, UpdateUserRequest request);
        Task DeleteUser(int id);
    }
}

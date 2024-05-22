using University.Domain.Entity.User;

namespace University.Domain.Repositores
{
    public interface IUserRepository
    {
        Task<UserEntity> CreateUser(UserEntity user);
        Task<UserEntity> UpdateUser(UserEntity user);
        Task<bool> ExistsById(int id);
        Task<UserEntity?> GetUserById(int id);
        Task<UserEntity?> GetUserByEmail(string email);
        Task<UserEntity?> GetUserByPhone(string phone);
        Task<UserEntity?> GetUserByTaxId(long taxId);
        Task<IQueryable<UserEntity>> GetAllUsers();
        Task DeleteUser(int id);
    }
}

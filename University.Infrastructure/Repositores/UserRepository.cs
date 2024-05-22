using Microsoft.EntityFrameworkCore;
using University.Domain.Entity.User;
using University.Domain.Exceptions;
using University.Domain.Repositores;

namespace University.Infrastructure.Repositores
{
    public class UserRepository : IUserRepository
    {
        protected readonly UniversityDbContext context;

        public UserRepository(UniversityDbContext context)
        {
            this.context = context;
        }

        public async Task<UserEntity> CreateUser(UserEntity user)
        {
            var result = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) throw new NotFoundException("User not found");
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }

        public async Task<bool> ExistsById(int id)
        {
            return await context.Users.AnyAsync(user => user.Id == id);
        }

        public Task<IQueryable<UserEntity>> GetAllUsers()
        {
            return Task.FromResult(context.Users.AsQueryable());
        }

        public async Task<UserEntity?> GetUserByEmail(string email)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<UserEntity?> GetUserById(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<UserEntity?> GetUserByPhone(string phone)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Phone == phone);
        }

        public async Task<UserEntity?> GetUserByTaxId(long taxId)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.TaxId == taxId);
        }

        public async Task<UserEntity> UpdateUser(UserEntity user)
        {
            var result = context.Users.Update(user);
            await context.SaveChangesAsync();

            return result.Entity;
        }
    }
}

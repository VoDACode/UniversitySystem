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
            var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var user = await context.Users
                    .FirstOrDefaultAsync(user => user.Id == id && !user.IsDeleted);
                if (user == null) throw new NotFoundException("User not found");

                user.Email = string.Empty;
                user.Phone = string.Empty;
                user.DateOfBirth = new DateOnly();
                user.TaxId = 0;
                user.FirstName = string.Empty;
                user.LastName = string.Empty;
                user.IsDeleted = true;

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ExistsById(int id)
        {
            return await context.Users.AnyAsync(user => user.Id == id && !user.IsDeleted);
        }

        public Task<IQueryable<UserEntity>> GetAllUsers()
        {
            return Task.FromResult(context.Users.Where(p => !p.IsDeleted).AsQueryable());
        }

        public async Task<UserEntity?> GetUserByEmail(string email)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Email == email && !user.IsDeleted);
        }

        public async Task<UserEntity?> GetUserById(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<UserEntity?> GetUserByPhone(string phone)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Phone == phone && !user.IsDeleted);
        }

        public async Task<UserEntity?> GetUserByTaxId(long taxId)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.TaxId == taxId && !user.IsDeleted);
        }

        public async Task<UserEntity> UpdateUser(UserEntity user)
        {
            if (user.IsDeleted) throw new NotFoundException("User not found");
            var result = context.Users.Update(user);
            await context.SaveChangesAsync();

            return result.Entity;
        }
    }
}

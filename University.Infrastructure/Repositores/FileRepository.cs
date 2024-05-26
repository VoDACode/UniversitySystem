using Microsoft.EntityFrameworkCore;
using University.Domain.Entity.File;
using University.Domain.Exceptions;
using University.Domain.Repositores;

namespace University.Infrastructure.Repositores
{
    public class FileRepository : IFileRepository
    {
        protected readonly UniversityDbContext context;

        public FileRepository(UniversityDbContext context)
        {
            this.context = context;
        }

        public async Task<FileEntity> CreateFile(FileEntity request)
        {
            var result = await context.Files.AddAsync(request);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteFile(long id)
        {
            var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var file = await context.Files.FindAsync(id);
                if (file == null) throw new NotFoundException("File not found");

                context.Files.Remove(file);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ExistsById(long id)
        {
            return await context.Files.AnyAsync(file => file.Id == id);
        }

        public async Task<FileEntity?> GetFileById(long id)
        {
            var file = await context.Files.FindAsync(id);
            return file;
        }
    }
}

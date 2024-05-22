using University.Domain.Entity.File;

namespace University.Domain.Repositores
{
    public interface IFileRepository
    {
        Task<bool> ExistsById(long id);
        Task<FileEntity?> GetFileById(long id);
        Task<FileEntity> CreateFile(FileEntity request);
        Task DeleteFile(long id);
    }
}

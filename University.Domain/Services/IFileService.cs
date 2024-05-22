using Microsoft.AspNetCore.Mvc;

namespace University.Domain.Services
{
    public interface IFileService
    {
        Task<FileStreamResult> DownloadFile(long id);
    }
}

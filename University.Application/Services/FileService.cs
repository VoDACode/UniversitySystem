using Microsoft.AspNetCore.Mvc;
using University.Domain.Exceptions;
using University.Domain.Repositores;
using University.Domain.Services;

namespace University.Application.Services
{
    public class FileService : IFileService
    {
        protected readonly IFileRepository fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            this.fileRepository = fileRepository;
        }

        public async Task<FileStreamResult> DownloadFile(long id)
        {
            if (!await fileRepository.ExistsById(id))
            {
                throw new NotFoundException("File not found");
            }

            var file = await fileRepository.GetFileById(id);

            throw new NotImplementedException("Before need implement the file storage setvice");
        }
    }
}

using Microsoft.AspNetCore.Http;
using University.Domain.Entity.File;
using University.Domain.Entity.TaskAnswer;
using University.Domain.Entity.TaskAnswer.Requests;
using University.Domain.Entity.TaskAnswer.Responses;
using University.Domain.Exceptions;
using University.Domain.Repositores;
using University.Domain.Services;

namespace University.Application.Services
{
    public class TaskAnswerService : ITaskAnswerService
    {
        protected readonly ITaskAnswerRepository taskAnswerRepository;
        protected readonly IFileRepository fileRepository;

        public TaskAnswerService(ITaskAnswerRepository taskAnswerRepository, IFileRepository fileRepository)
        {
            this.taskAnswerRepository = taskAnswerRepository;
            this.fileRepository = fileRepository;
        }

        public async Task<TaskAnswerResponse> CreateTaskAnswer(CreateTaskAnswerRequest request, IFormFileCollection fileCollection)
        {
            TaskAnswerEntity taskAnswer = new TaskAnswerEntity
            {
                TaskId = request.TaskId,
                StudentId = request.StudentId
            };

            TaskAnswerEntity createdTaskAnswer = await taskAnswerRepository.CreateTaskAnswer(taskAnswer);

            foreach (IFormFile file in fileCollection)
            {
                FileEntity fileEntity = new FileEntity
                {
                    TaskAnswerId = createdTaskAnswer.Id,
                    Name = file.FileName,
                    Size = file.Length,
                    OwnerId = request.StudentId
                };

                fileEntity = await fileRepository.CreateFile(fileEntity);
                createdTaskAnswer.Files.Add(fileEntity);
            }

            return createdTaskAnswer;
        }

        public async Task DeleteTaskAnswer(int id)
        {
            if(!await taskAnswerRepository.ExistsById(id))
            {
                throw new BadRequestException("Task answer not found");
            }

            await taskAnswerRepository.DeleteTaskAnswer(id);
        }

        public async Task<TaskAnswerResponse> EvaluateTaskAnswer(int id, EvaluateTaskAnswerRequest request)
        {
            TaskAnswerEntity? taskAnswer = await taskAnswerRepository.GetTaskAnswerById(id);

            if(taskAnswer == null)
            {
                throw new BadRequestException("Task answer not found");
            }

            if (request.Mark > taskAnswer.Task.MaxMark)
            {
                throw new BadRequestException("Mark is greater than max mark");
            }

            taskAnswer.Mark = request.Mark;
            taskAnswer.Feedback = request.Feedback;
            taskAnswer.MarkedAt = DateTime.UtcNow;

            return await taskAnswerRepository.UpdateTaskAnswer(taskAnswer);
        }

        public async Task<TaskAnswerResponse> GetTaskAnswer(int id)
        {
            TaskAnswerEntity? taskAnswer = await taskAnswerRepository.GetTaskAnswerById(id);

            if (taskAnswer == null)
            {
                throw new BadRequestException("Task answer not found");
            }

            return taskAnswer;
        }

        public async Task<TaskAnswerResponse> UpdateTaskAnswer(int id, UpdateTaskAnswerRequest request, IFormFileCollection fileCollection)
        {
            TaskAnswerEntity? taskAnswer = await taskAnswerRepository.GetTaskAnswerById(id);

            if (taskAnswer == null)
            {
                throw new BadRequestException("Task answer not found");
            }

            foreach (var fileId in request.DeleteFiles)
            {
                FileEntity? fileEntity = await fileRepository.GetFileById(fileId);
                if(fileEntity == null)
                {
                    throw new BadRequestException($"File [{fileId}] not found");
                }
                if(fileEntity.OwnerId != taskAnswer.StudentId)
                {
                    throw new BadRequestException($"File [{fileId}] does not belong to the student");
                }

                taskAnswer.Files.Remove(fileEntity);
            }

            if(taskAnswer.Files.Count + fileCollection.Count > taskAnswer.Task.MaxFiles)
            {
                throw new BadRequestException($"Max files count is {taskAnswer.Task.MaxFiles}");
            }

            if(taskAnswer.Files.Count + fileCollection.Count == 0)
            {
                throw new BadRequestException("Files are required");
            }

            foreach (IFormFile file in fileCollection)
            {
                FileEntity fileEntity = new FileEntity
                {
                    TaskAnswerId = taskAnswer.Id,
                    Name = file.FileName,
                    Size = file.Length,
                    OwnerId = taskAnswer.StudentId
                };

                fileEntity = await fileRepository.CreateFile(fileEntity);
                taskAnswer.Files.Add(fileEntity);
            }

            return await taskAnswerRepository.UpdateTaskAnswer(taskAnswer);
        }
    }
}

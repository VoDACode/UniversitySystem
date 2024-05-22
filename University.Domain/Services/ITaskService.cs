using University.Domain.Entity.Task.Requests;
using University.Domain.Entity.Task.Responses;
using University.Domain.Entity.TaskAnswer.Responses;
using University.Domain.Requests;
using University.Domain.Responses;

namespace University.Domain.Services
{
    public interface ITaskService
    {
        Task<TaskResponse> GetTask(int id);
        Task<PageResponse<TaskResponse>> GetTasks(PageRequest request);
        Task<IEnumerable<TaskAnswerResponse>> GetTaskAnswersFromTask(int id);
        Task<TaskResponse> CreateTask(CreateTaskRequest request);
        Task<TaskResponse> UpdateTask(int id, UpdateTaskRequest request);
        Task DeleteTask(int id);
    }
}

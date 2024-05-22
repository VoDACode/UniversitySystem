using Microsoft.AspNetCore.Http;
using University.Domain.Entity.TaskAnswer.Requests;
using University.Domain.Entity.TaskAnswer.Responses;

namespace University.Domain.Services
{
    public interface ITaskAnswerService
    {
        Task<TaskAnswerResponse> CreateTaskAnswer(CreateTaskAnswerRequest request, IFormFileCollection fileCollection);
        Task<TaskAnswerResponse> GetTaskAnswer(int id);
        Task<TaskAnswerResponse> EvaluateTaskAnswer(int id, EvaluateTaskAnswerRequest request);
        Task<TaskAnswerResponse> UpdateTaskAnswer(int id, UpdateTaskAnswerRequest request, IFormFileCollection fileCollection);
        Task DeleteTaskAnswer(int id);
    }
}

using University.Domain.Entity.TaskAnswer;

namespace University.Domain.Repositores
{
    public interface ITaskAnswerRepository
    {
        Task<bool> ExistsById(int id);
        Task<TaskAnswerEntity> CreateTaskAnswer(TaskAnswerEntity request);
        Task<TaskAnswerEntity> UpdateTaskAnswer(TaskAnswerEntity request);
        Task<TaskAnswerEntity?> GetTaskAnswerById(int id);
        Task DeleteTaskAnswer(int id);
    }
}

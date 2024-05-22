using University.Domain.Entity.Task;

namespace University.Domain.Repositores
{
    public interface ITaskRepository
    {
        Task<TaskEntity> CreateTask(TaskEntity task);
        Task<TaskEntity> UpdateTask(TaskEntity task);
        Task<bool> ExistById(int id);
        Task<TaskEntity> GetTaskById(int id);
        Task<TaskEntity> GetTaskAnswersFromTask(int id);
        Task<IQueryable<TaskEntity>> GetAllTasks();
        Task DeleteTask(int id);
    }
}

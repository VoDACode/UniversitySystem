using Microsoft.EntityFrameworkCore;
using University.Domain.Entity.Task;
using University.Domain.Exceptions;
using University.Domain.Repositores;

namespace University.Infrastructure.Repositores
{
    public class TaskRepository : ITaskRepository
    {
        protected readonly UniversityDbContext context;

        public TaskRepository(UniversityDbContext context)
        {
            this.context = context;
        }

        public async Task<TaskEntity> CreateTask(TaskEntity task)
        {
            var result = await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteTask(int id)
        {
            var task = await context.Tasks.FindAsync(id);
            if (task == null) throw new NotFoundException("Task not found");
            context.Tasks.Remove(task);
            await context.SaveChangesAsync();
        }

        public async Task<bool> ExistById(int id)
        {
            return await context.Tasks.AnyAsync(task => task.Id == id);
        }

        public Task<IQueryable<TaskEntity>> GetAllTasks()
        {
            return Task.FromResult(context.Tasks.AsQueryable());
        }

        public async Task<TaskEntity> GetTaskAnswersFromTask(int id)
        {
            var task = await context.Tasks
                .Include(t => t.TaskAnswers)
                .ThenInclude(ta => ta.Files)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null) throw new NotFoundException("Task not found");

            return task;
        }

        public async Task<TaskEntity> GetTaskById(int id)
        {
            var task = await context.Tasks.FindAsync(id);
            if (task == null) throw new NotFoundException("Task not found");
            return task;
        }

        public async Task<TaskEntity> UpdateTask(TaskEntity task)
        {
            var result = context.Tasks.Update(task);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}

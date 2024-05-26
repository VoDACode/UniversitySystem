using Microsoft.EntityFrameworkCore;
using University.Domain.Entity.Task;
using University.Domain.Exceptions;
using University.Domain.Repositores;

namespace University.Infrastructure.Repositores
{
    public class TaskRepository : ITaskRepository
    {
        protected readonly ITaskAnswerRepository taskAnswerRepository;
        protected readonly UniversityDbContext context;

        public TaskRepository(UniversityDbContext context, ITaskAnswerRepository taskAnswerRepository)
        {
            this.context = context;
            this.taskAnswerRepository = taskAnswerRepository;
        }

        public async Task<TaskEntity> CreateTask(TaskEntity task)
        {
            var result = await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteTask(int id)
        {
            var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var task = await context.Tasks
                    .Include(t => t.TaskAnswers).ThenInclude(ta => ta.Files)
                    .FirstOrDefaultAsync(t => t.Id == id);
                if (task == null) throw new NotFoundException("Task not found");

                context.Files.RemoveRange(task.TaskAnswers.SelectMany(ta => ta.Files));
                await context.SaveChangesAsync();

                context.TaskAnswers.RemoveRange(task.TaskAnswers);
                await context.SaveChangesAsync();

                context.Tasks.Remove(task);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
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

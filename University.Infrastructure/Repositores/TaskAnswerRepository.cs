using Microsoft.EntityFrameworkCore;
using University.Domain.Entity.TaskAnswer;
using University.Domain.Exceptions;
using University.Domain.Repositores;

namespace University.Infrastructure.Repositores
{
    public class TaskAnswerRepository : ITaskAnswerRepository
    {
        protected readonly UniversityDbContext context;

        public TaskAnswerRepository(UniversityDbContext context)
        {
            this.context = context;
        }

        public async Task<TaskAnswerEntity> CreateTaskAnswer(TaskAnswerEntity request)
        {
            var result = await context.TaskAnswers.AddAsync(request);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteTaskAnswer(int id)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var taskAnswer = await context.TaskAnswers
                    .Include(p => p.Files)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (taskAnswer == null) throw new NotFoundException("TaskAnswer not found");
                
                context.Files.RemoveRange(taskAnswer.Files);
                await context.SaveChangesAsync();

                context.TaskAnswers.Remove(taskAnswer);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ExistsById(int id)
        {
            return await context.TaskAnswers.AnyAsync(taskAnswer => taskAnswer.Id == id);
        }

        public async Task<TaskAnswerEntity?> GetTaskAnswerById(int id)
        {
            var taskAnswer = await context.TaskAnswers.Include(p => p.Task).FirstOrDefaultAsync(p => p.Id == id);
            return taskAnswer;
        }

        public async Task<TaskAnswerEntity> UpdateTaskAnswer(TaskAnswerEntity request)
        {
            var result = context.TaskAnswers.Update(request);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}

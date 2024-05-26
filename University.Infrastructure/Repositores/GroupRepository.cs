using Microsoft.EntityFrameworkCore;
using University.Domain.Entity.Group;
using University.Domain.Exceptions;
using University.Domain.Repositores;

namespace University.Infrastructure.Repositores
{
    public class GroupRepository : IGroupRepository
    {
        protected readonly UniversityDbContext context;

        public GroupRepository(UniversityDbContext context)
        {
            this.context = context;
        }

        public async Task<GroupEntity> CreateGroup(GroupEntity group)
        {
            group = (await context.Groups.AddAsync(group)).Entity;
            await context.SaveChangesAsync();
            return group;
        }

        public async Task DeleteGroup(int id)
        {
            var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var group = await context.Groups
                    .Include(x => x.Lessons)
                    .Include(x => x.Tasks).ThenInclude(x => x.TaskAnswers).ThenInclude(x => x.Files)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (group == null) throw new NotFoundException("Group not found");

                context.Lessons.RemoveRange(group.Lessons);
                await context.SaveChangesAsync();

                context.Files.RemoveRange(group.Tasks.SelectMany(x => x.TaskAnswers).SelectMany(x => x.Files));
                await context.SaveChangesAsync();

                context.TaskAnswers.RemoveRange(group.Tasks.SelectMany(x => x.TaskAnswers));
                await context.SaveChangesAsync();

                context.Tasks.RemoveRange(group.Tasks);
                await context.SaveChangesAsync();

                group.Students.Clear();
                await context.SaveChangesAsync();

                context.Groups.Remove(group);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ExistsById(int id)
        {
            return await context.Groups.AnyAsync(x => x.Id == id);
        }

        public async Task<GroupEntity?> GetCoursesFromGroup(int id)
        {
            return await context.Groups
                .Include(x => x.Courses)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<GroupEntity?> GetGroupById(int id)
        {
            return await context.Groups.FindAsync(id);
        }

        public Task<IQueryable<GroupEntity>> GetGroups()
        {
            return Task.FromResult(context.Groups.AsQueryable());
        }

        public async Task<GroupEntity?> GetLessonsFromGroup(int id)
        {
            return await context.Groups
                .Include(x => x.Lessons)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<GroupEntity?> GetStudentsFromGroup(int id)
        {
            return await context.Groups
                .Include(x => x.Students)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<GroupEntity?> GetTasksFromGroup(int id)
        {
            return await context.Groups
                .Include(x => x.Tasks)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<GroupEntity> UpdateGroup(GroupEntity group)
        {
            var result = context.Groups.Update(group);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}

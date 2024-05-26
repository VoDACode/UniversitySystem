using Microsoft.EntityFrameworkCore;
using University.Domain.Entity.Course;
using University.Domain.Exceptions;
using University.Domain.Repositores;

namespace University.Infrastructure.Repositores
{
    public class CourseRepository : ICourseRepository
    {
        protected readonly UniversityDbContext context;

        public CourseRepository(UniversityDbContext context)
        {
            this.context = context;
        }

        public async Task<CourseEntity> CreateCourse(CourseEntity request)
        {
            var result = await context.Courses.AddAsync(request);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteCourse(int id)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var course = await context.Courses
                    .Include(p => p.Groups)
                    .Include(p => p.Lessons)
                    .Include(p => p.Tasks).ThenInclude(p => p.TaskAnswers).ThenInclude(p => p.Files)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (course == null) throw new NotFoundException("Course not found");

                context.Files.RemoveRange(course.Tasks.SelectMany(p => p.TaskAnswers).SelectMany(p => p.Files));
                await context.SaveChangesAsync();

                context.TaskAnswers.RemoveRange(course.Tasks.SelectMany(p => p.TaskAnswers));
                await context.SaveChangesAsync();

                context.Tasks.RemoveRange(course.Tasks);
                await context.SaveChangesAsync();

                course.Groups.Clear();
                await context.SaveChangesAsync();

                context.Lessons.RemoveRange(course.Lessons);
                await context.SaveChangesAsync();

                context.Courses.Remove(course);
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
            return await context.Courses.AnyAsync(course => course.Id == id);
        }

        public async Task<CourseEntity?> GetCourseById(int id)
        {
            var course = await context.Courses.FindAsync(id);
            return course;
        }

        public async Task<CourseEntity?> GetCourseGroupsById(int id)
        {
            var course = await context.Courses
                .Include(p => p.Groups)
                .FirstOrDefaultAsync(p => p.Id == id);
            return course;
        }

        public Task<IQueryable<CourseEntity>> GetCourses()
        {
            return Task.FromResult(context.Courses.AsQueryable());
        }

        public async Task<CourseEntity> UpdateCourse(CourseEntity request)
        {
            var result = context.Courses.Update(request);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}

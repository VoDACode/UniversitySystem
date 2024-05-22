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
            var course = await context.Courses.FindAsync(id);
            if (course == null) throw new NotFoundException("Course not found");
            context.Courses.Remove(course);
            await context.SaveChangesAsync();
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

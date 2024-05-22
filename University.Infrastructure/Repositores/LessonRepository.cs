using Microsoft.EntityFrameworkCore;
using University.Domain.Entity.Lesson;
using University.Domain.Exceptions;
using University.Domain.Repositores;

namespace University.Infrastructure.Repositores
{
    public class LessonRepository : ILessonRepository
    {
        protected readonly UniversityDbContext context;

        public LessonRepository(UniversityDbContext context)
        {
            this.context = context;
        }

        public async Task<LessonEntity> CreateLesson(LessonEntity lesson)
        {
            var result = await context.Lessons.AddAsync(lesson);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteLesson(int id)
        {
            var lesson = await context.Lessons.FindAsync(id);
            if (lesson == null) throw new NotFoundException("Lesson not found");
            context.Lessons.Remove(lesson);
            await context.SaveChangesAsync();
        }

        public async Task<bool> ExistsById(int id)
        {
            return await context.Lessons.AnyAsync(lesson => lesson.Id == id);
        }

        public async Task<LessonEntity?> GetLessonById(int id)
        {
            var lesson = await context.Lessons.FindAsync(id);
            return lesson;
        }

        public Task<IQueryable<LessonEntity>> GetLessons()
        {
            return Task.FromResult(context.Lessons.AsQueryable());
        }

        public async Task<LessonEntity> UpdateLesson(LessonEntity lesson)
        {
            var result = context.Lessons.Update(lesson);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}

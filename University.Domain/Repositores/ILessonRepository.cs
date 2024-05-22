using University.Domain.Entity.Lesson;

namespace University.Domain.Repositores
{
    public interface ILessonRepository
    {
        Task<LessonEntity> CreateLesson(LessonEntity lesson);
        Task<LessonEntity> UpdateLesson(LessonEntity lesson);
        Task<LessonEntity?> GetLessonById(int id);
        Task<bool> ExistsById(int id);
        Task<IQueryable<LessonEntity>> GetLessons();
        Task DeleteLesson(int id);
    }
}

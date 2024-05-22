using University.Domain.Entity.Lesson.Requests;
using University.Domain.Entity.Lesson.Responses;

namespace University.Domain.Services
{
    public interface ILessonService
    {
        Task<LessonResponse> CreateLesson(CreateLessonRequest request);
        Task<LessonResponse> UpdateLesson(int id, UpdateLessonRequest request);
        Task<LessonResponse> GetLesson(int id);
        Task<IEnumerable<LessonResponse>> GetLessons();
        Task DeleteLesson(int id);
    }
}

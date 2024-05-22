using University.Domain.Entity.Lesson.Requests;
using University.Domain.Entity.Lesson.Responses;
using University.Domain.Requests;
using University.Domain.Responses;

namespace University.Domain.Services
{
    public interface ILessonService
    {
        Task<LessonResponse> CreateLesson(CreateLessonRequest request);
        Task<LessonResponse> UpdateLesson(int id, UpdateLessonRequest request);
        Task<LessonResponse> GetLesson(int id);
        Task<PageResponse<LessonResponse>> GetLessons(PageRequest request);
        Task DeleteLesson(int id);
    }
}

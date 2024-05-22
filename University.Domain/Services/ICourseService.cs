using University.Domain.Entity.Course.Requests;
using University.Domain.Entity.Course.Responses;

namespace University.Domain.Services
{
    public interface ICourseService
    {
        Task<IList<CourseResponse>> GetAllCourses();
        Task<CourseResponse> GetCourseById(int id);
        Task<CourseGroupsResponse> GetCourseGroupsById(int id);
        Task<CourseResponse> CreateCourse(CreateCourseRequest request);
        Task<CourseResponse> UpdateCourse(int id, UpdateCourseRequest request);
        Task DeleteCourse(int id);
    }
}

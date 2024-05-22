using University.Domain.Entity.Course.Requests;
using University.Domain.Entity.Course.Responses;
using University.Domain.Requests;
using University.Domain.Responses;

namespace University.Domain.Services
{
    public interface ICourseService
    {
        Task<PageResponse<CourseResponse>> GetCourses(PageRequest request);
        Task<CourseResponse> GetCourseById(int id);
        Task<CourseGroupsResponse> GetCourseGroupsById(int id);
        Task<CourseResponse> CreateCourse(CreateCourseRequest request);
        Task<CourseResponse> UpdateCourse(int id, UpdateCourseRequest request);
        Task DeleteCourse(int id);
    }
}

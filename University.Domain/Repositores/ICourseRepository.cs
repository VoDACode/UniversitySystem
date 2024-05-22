using University.Domain.Entity.Course;

namespace University.Domain.Repositores
{
    public interface ICourseRepository
    {
        Task<bool> ExistsById(int id);
        Task<IQueryable<CourseEntity>> GetCourses();
        Task<CourseEntity?> GetCourseById(int id);
        Task<CourseEntity?> GetCourseGroupsById(int id);
        Task<CourseEntity> CreateCourse(CourseEntity request);
        Task<CourseEntity> UpdateCourse(CourseEntity request);
        Task DeleteCourse(int id);
    }
}

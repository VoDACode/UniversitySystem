using University.Domain.Entity.Course;
using University.Domain.Entity.Course.Requests;
using University.Domain.Entity.Course.Responses;
using University.Domain.Exceptions;
using University.Domain.Repositores;
using University.Domain.Requests;
using University.Domain.Responses;
using University.Domain.Services;

namespace University.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task<CourseResponse> CreateCourse(CreateCourseRequest request)
        {
            var course = new CourseEntity
            {
                Name = request.Name
            };

            return await courseRepository.CreateCourse(course);
        }

        public async Task DeleteCourse(int id)
        {
            if (!await courseRepository.ExistsById(id))
            {
                throw new NotFoundException("Course not found");
            }

            await courseRepository.DeleteCourse(id);
        }

        public async Task<PageResponse<CourseResponse>> GetCourses(PageRequest request)
        {
            IQueryable<CourseEntity> courses = await courseRepository.GetCourses();
            IQueryable<CourseResponse> courseResponses = courses.Select(course => new CourseResponse(course));

            return await PageResponse<CourseResponse>.Create(courseResponses, request);
        }

        public async Task<CourseResponse> GetCourseById(int id)
        {
            if (!await courseRepository.ExistsById(id))
            {
                throw new NotFoundException("Course not found");
            }

            var course = await courseRepository.GetCourseById(id);

            return course ?? throw new NotFoundException("Course not found");
        }

        public async Task<CourseGroupsResponse> GetCourseGroupsById(int id)
        {
            if(!await courseRepository.ExistsById(id))
            {
                throw new NotFoundException("Course not found");
            }

            var course = await courseRepository.GetCourseGroupsById(id);

            return course ?? throw new NotFoundException("Course not found");
        }

        public async Task<CourseResponse> UpdateCourse(int id, UpdateCourseRequest request)
        {
            if (!await courseRepository.ExistsById(id))
            {
                throw new NotFoundException("Course not found");
            }

            var course = await courseRepository.GetCourseById(id);

            if(course == null)
            {
                throw new NotFoundException("Course not found");
            }

            course.Name = request.Name;

            return await courseRepository.UpdateCourse(course);
        }
    }
}

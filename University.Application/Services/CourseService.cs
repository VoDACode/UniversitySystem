using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
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
        protected readonly IDistributedCache cache;

        protected const string CacheEntryKeyFormat = "Course_{0}";
        protected const string CacheCourseGroupsKeyFormat = "CourseGroups_{0}";
        protected const string CachePageKeyFormat = "Courses_{0}_{1}";

        public CourseService(ICourseRepository courseRepository, IDistributedCache cache)
        {
            this.courseRepository = courseRepository;
            this.cache = cache;
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
            await cache.RemoveAsync(string.Format(CacheEntryKeyFormat, id));
        }

        public async Task<PageResponse<CourseResponse>> GetCourses(PageRequest request)
        {
            PageResponse<CourseResponse>? pageResponse;

            var cacheResponse = await cache.GetStringAsync(string.Format(CachePageKeyFormat, request.Page));
            if (cacheResponse != null)
            {
                pageResponse = JsonSerializer.Deserialize<PageResponse<CourseResponse>>(cacheResponse);
                if (pageResponse != null)
                {
                    return pageResponse;
                }
                await cache.RemoveAsync(string.Format(CachePageKeyFormat, request.Page));
            }

            IQueryable<CourseEntity> courses = await courseRepository.GetCourses();
            IQueryable<CourseResponse> courseResponses = courses.Select(course => new CourseResponse(course));

            pageResponse = await PageResponse<CourseResponse>.Create(courseResponses, request);
            if (pageResponse.Items.Any())
            {
                await cache.SetStringAsync(string.Format(CachePageKeyFormat, request.Page), JsonSerializer.Serialize(pageResponse), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }

            return pageResponse;
        }

        public async Task<CourseResponse> GetCourseById(int id)
        {
            var cacheCourse = await cache.GetStringAsync(string.Format(CacheEntryKeyFormat, id));
            if (cacheCourse != null)
            {
                var courseResponse = JsonSerializer.Deserialize<CourseResponse>(cacheCourse);
                if (courseResponse != null)
                {
                    return courseResponse;
                }
                await cache.RemoveAsync(string.Format(CacheEntryKeyFormat, id));
            }

            var course = await courseRepository.GetCourseById(id);
            if(course == null)
            {
                throw new NotFoundException("Course not found");
            }

            CourseResponse response = course;
            await cache.SetStringAsync(string.Format(CacheEntryKeyFormat, id), JsonSerializer.Serialize(response), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            return response;
        }

        public async Task<CourseGroupsResponse> GetCourseGroupsById(int id)
        {
            var cacheCourse = await cache.GetStringAsync(string.Format(CacheCourseGroupsKeyFormat, id));
            if (cacheCourse != null)
            {
                var courseResponse = JsonSerializer.Deserialize<CourseGroupsResponse>(cacheCourse);
                if (courseResponse != null)
                {
                    return courseResponse;
                }
                await cache.RemoveAsync(string.Format(CacheCourseGroupsKeyFormat, id));
            }

            var course = await courseRepository.GetCourseGroupsById(id);
            if (course == null)
            {
                throw new NotFoundException("Course not found");
            }

            CourseGroupsResponse response = course;
            await cache.SetStringAsync(string.Format(CacheCourseGroupsKeyFormat, id), JsonSerializer.Serialize(response), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            return response;
        }

        public async Task<CourseResponse> UpdateCourse(int id, UpdateCourseRequest request)
        {
            if (!await courseRepository.ExistsById(id))
            {
                throw new NotFoundException("Course not found");
            }

            var course = await courseRepository.GetCourseById(id);

            if (course == null)
            {
                throw new NotFoundException("Course not found");
            }

            course.Name = request.Name;

            await cache.RemoveAsync(string.Format(CacheEntryKeyFormat, id));
            await cache.RemoveAsync(string.Format(CacheCourseGroupsKeyFormat, id));

            return await courseRepository.UpdateCourse(course);
        }
    }
}

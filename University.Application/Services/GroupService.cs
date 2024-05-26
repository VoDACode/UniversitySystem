using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using University.Domain.Entity.Course.Responses;
using University.Domain.Entity.Group;
using University.Domain.Entity.Group.Requests;
using University.Domain.Entity.Group.Responses;
using University.Domain.Entity.Lesson.Responses;
using University.Domain.Entity.Task.Responses;
using University.Domain.Entity.User.Responses;
using University.Domain.Exceptions;
using University.Domain.Repositores;
using University.Domain.Requests;
using University.Domain.Responses;
using University.Domain.Services;

namespace University.Application.Services
{
    public class GroupService : IGroupService
    {
        protected readonly IGroupRepository groupRepository;
        protected readonly IUserRepository userRepository;
        protected readonly IDistributedCache cache;

        protected const string CacheGroupKeyFormat = "Group_{0}";
        protected const string CacheGroupsPageKeyFormat = "Group_{0}_{1}";
        protected const string CacheGroupCoursesKeyFormat = "GroupCourses_{0}";
        protected const string CacheGroupLessonsKeyFormat = "GroupLessons_{0}";
        protected const string CacheGroupTasksKeyFormat = "GroupTasks_{0}";
        protected const string CacheGroupStudentsKeyFormat = "GroupStudents_{0}";

        public GroupService(IGroupRepository groupRepository, IUserRepository userRepository, IDistributedCache cache)
        {
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
            this.cache = cache;
        }

        public async Task<GroupResponse> CreateGroup(CreateGroupRequest request)
        {
            var teacher = await userRepository.GetUserById(request.TeacherId);
            if (teacher == null)
            {
                throw new BadRequestException("Teacher not found");
            }

            var group = new GroupEntity
            {
                Name = request.Name,
                Teacher = teacher,
                IsSubGroup = request.IsSubGroup
            };

            group = await groupRepository.CreateGroup(group);

            return new GroupResponse(group);
        }

        public async Task DeleteGroup(int id)
        {
            if (!await groupRepository.ExistsById(id))
            {
                throw new NotFoundException("Group not found");
            }

            await groupRepository.DeleteGroup(id);

            await cache.RemoveAsync(string.Format(CacheGroupKeyFormat, id));
            await cache.RemoveAsync(string.Format(CacheGroupStudentsKeyFormat, id));
            await cache.RemoveAsync(string.Format(CacheGroupCoursesKeyFormat, id));
            await cache.RemoveAsync(string.Format(CacheGroupLessonsKeyFormat, id));
            await cache.RemoveAsync(string.Format(CacheGroupTasksKeyFormat, id));
        }

        public async Task<IList<CourseResponse>> GetCoursesFromGroup(int id)
        {
            var cacheGroup = await cache.GetStringAsync(string.Format(CacheGroupCoursesKeyFormat, id));
            if (cacheGroup != null)
            {
                var courseResponse = JsonSerializer.Deserialize<IList<CourseResponse>>(cacheGroup);
                if (courseResponse != null)
                {
                    return courseResponse;
                }
                await cache.RemoveAsync(string.Format(CacheGroupCoursesKeyFormat, id));
            }

            var group = await groupRepository.GetCoursesFromGroup(id);
            if (group == null)
            {
                throw new NotFoundException("Course not found");
            }

            IList<CourseResponse> response = group.Courses.Select(course => new CourseResponse(course)).ToList();
            await cache.SetStringAsync(string.Format(CacheGroupCoursesKeyFormat, id), JsonSerializer.Serialize(response), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            return response;
        }

        public async Task<GroupResponse> GetGroup(int id)
        {
            var cacheGroup = await cache.GetStringAsync(string.Format(CacheGroupKeyFormat, id));
            if (cacheGroup != null)
            {
                var courseResponse = JsonSerializer.Deserialize<GroupResponse>(cacheGroup);
                if (courseResponse != null)
                {
                    return courseResponse;
                }
                await cache.RemoveAsync(string.Format(CacheGroupKeyFormat, id));
            }

            var group = await groupRepository.GetGroupById(id);
            if (group == null)
            {
                throw new NotFoundException("Course not found");
            }

            GroupResponse response = group;
            await cache.SetStringAsync(string.Format(CacheGroupKeyFormat, id), JsonSerializer.Serialize(response), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            return response;
        }

        public async Task<PageResponse<GroupResponse>> GetGroups(PageRequest request)
        {
            PageResponse<GroupResponse>? pageResponse;

            var cacheResponse = await cache.GetStringAsync(string.Format(CacheGroupsPageKeyFormat, request.Page));
            if (cacheResponse != null)
            {
                pageResponse = JsonSerializer.Deserialize<PageResponse<GroupResponse>>(cacheResponse);
                if (pageResponse != null)
                {
                    return pageResponse;
                }
                await cache.RemoveAsync(string.Format(CacheGroupsPageKeyFormat, request.Page));
            }

            IQueryable<GroupEntity> groups = await groupRepository.GetGroups();
            IQueryable<GroupResponse> groupsResponse = groups.Select(group => new GroupResponse(group));

            pageResponse = await PageResponse<GroupResponse>.Create(groupsResponse, request);
            if (pageResponse.Items.Any())
            {
                await cache.SetStringAsync(string.Format(CacheGroupsPageKeyFormat, request.Page), JsonSerializer.Serialize(pageResponse), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }

            return pageResponse;
        }

        public async Task<IList<LessonResponse>> GetLessonsFromGroup(int id)
        {
            var cacheGroup = await cache.GetStringAsync(string.Format(CacheGroupLessonsKeyFormat, id));
            if (cacheGroup != null)
            {
                var courseResponse = JsonSerializer.Deserialize<IList<LessonResponse>>(cacheGroup);
                if (courseResponse != null)
                {
                    return courseResponse;
                }
                await cache.RemoveAsync(string.Format(CacheGroupLessonsKeyFormat, id));
            }

            var group = await groupRepository.GetLessonsFromGroup(id);
            if (group == null)
            {
                throw new NotFoundException("Course not found");
            }

            IList<LessonResponse> response = group.Lessons.Select(course => new LessonResponse(course)).ToList();
            await cache.SetStringAsync(string.Format(CacheGroupLessonsKeyFormat, id), JsonSerializer.Serialize(response), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            return response;
        }

        public async Task<IList<UserResponse>> GetStudentsFromGroup(int id)
        {
            var cacheGroup = await cache.GetStringAsync(string.Format(CacheGroupStudentsKeyFormat, id));
            if (cacheGroup != null)
            {
                var courseResponse = JsonSerializer.Deserialize<IList<UserResponse>>(cacheGroup);
                if (courseResponse != null)
                {
                    return courseResponse;
                }
                await cache.RemoveAsync(string.Format(CacheGroupStudentsKeyFormat, id));
            }

            var group = await groupRepository.GetStudentsFromGroup(id);
            if (group == null)
            {
                throw new NotFoundException("Course not found");
            }

            IList<UserResponse> response = group.Students.Select(course => new UserResponse(course)).ToList();
            await cache.SetStringAsync(string.Format(CacheGroupStudentsKeyFormat, id), JsonSerializer.Serialize(response), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            return response;
        }

        public async Task<IList<TaskResponse>> GetTasksFromGroup(int id)
        {
            var cacheGroup = await cache.GetStringAsync(string.Format(CacheGroupTasksKeyFormat, id));
            if (cacheGroup != null)
            {
                var courseResponse = JsonSerializer.Deserialize<IList<TaskResponse>>(cacheGroup);
                if (courseResponse != null)
                {
                    return courseResponse;
                }
                await cache.RemoveAsync(string.Format(CacheGroupTasksKeyFormat, id));
            }

            var group = await groupRepository.GetTasksFromGroup(id);
            if (group == null)
            {
                throw new NotFoundException("Course not found");
            }

            IList<TaskResponse> response = group.Tasks.Select(course => new TaskResponse(course)).ToList();
            await cache.SetStringAsync(string.Format(CacheGroupTasksKeyFormat, id), JsonSerializer.Serialize(response), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            return response;
        }

        public async Task<GroupResponse> UpdateGroup(int id, UpdateGroupRequest request)
        {
            var group = await groupRepository.GetGroupById(id);
            if (group == null)
            {
                throw new NotFoundException("Group not found");
            }

            group.Name = request.Name;
            group.IsSubGroup = request.IsSubGroup;

            group = await groupRepository.UpdateGroup(group);

            await cache.RemoveAsync(string.Format(CacheGroupKeyFormat, id));
            await cache.RemoveAsync(string.Format(CacheGroupStudentsKeyFormat, id));
            await cache.RemoveAsync(string.Format(CacheGroupCoursesKeyFormat, id));
            await cache.RemoveAsync(string.Format(CacheGroupLessonsKeyFormat, id));
            await cache.RemoveAsync(string.Format(CacheGroupTasksKeyFormat, id));

            return group;
        }
    }
}

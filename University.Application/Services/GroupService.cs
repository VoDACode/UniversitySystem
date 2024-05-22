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

        public GroupService(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
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
        }

        public async Task<IList<CourseResponse>> GetCoursesFromGroup(int id)
        {
            var group = await groupRepository.GetCoursesFromGroup(id);
            if (group == null)
            {
                throw new NotFoundException("Group not found");
            }

            return group.Courses.Select(course => new CourseResponse(course)).ToList();
        }

        public async Task<GroupResponse> GetGroup(int id)
        {
            var group = await groupRepository.GetGroupById(id);
            if (group == null)
            {
                throw new NotFoundException("Group not found");
            }

            return group;
        }

        public async Task<PageResponse<GroupResponse>> GetGroups(PageRequest request)
        {
            IQueryable<GroupEntity> groups = await groupRepository.GetGroups();
            IQueryable<GroupResponse> groupsResponse = groups.Select(group => new GroupResponse(group));

            return await PageResponse<GroupResponse>.Create(groupsResponse, request);
        }

        public async Task<IList<LessonResponse>> GetLessonsFromGroup(int id)
        {
            var group = await groupRepository.GetLessonsFromGroup(id);
            if (group == null)
            {
                throw new NotFoundException("Group not found");
            }

            return group.Lessons.Select(lesson => new LessonResponse(lesson)).ToList();
        }

        public async Task<IList<UserResponse>> GetStudentsFromGroup(int id)
        {
            var group = await groupRepository.GetStudentsFromGroup(id);
            if (group == null)
            {
                throw new NotFoundException("Group not found");
            }

            return group.Students.Select(student => new UserResponse(student)).ToList();
        }

        public async Task<IList<TaskResponse>> GetTasksFromGroup(int id)
        {
            var group = await groupRepository.GetTasksFromGroup(id);
            if (group == null)
            {
                throw new NotFoundException("Group not found");
            }

            return group.Tasks.Select(task => new TaskResponse(task)).ToList();
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

            return group;
        }
    }
}

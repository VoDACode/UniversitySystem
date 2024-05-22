using University.Domain.Entity.Course.Responses;
using University.Domain.Entity.Group.Requests;
using University.Domain.Entity.Group.Responses;
using University.Domain.Entity.Lesson.Responses;
using University.Domain.Entity.Task.Responses;
using University.Domain.Entity.User.Responses;
using University.Domain.Requests;
using University.Domain.Responses;

namespace University.Domain.Services
{
    public interface IGroupService
    {
        Task<GroupResponse> GetGroup(int id);
        Task<PageResponse<GroupResponse>> GetGroups(PageRequest request);
        Task<IList<UserResponse>> GetStudentsFromGroup(int id);
        Task<IList<LessonResponse>> GetLessonsFromGroup(int id);
        Task<IList<TaskResponse>> GetTasksFromGroup(int id);
        Task<IList<CourseResponse>> GetCoursesFromGroup(int id);
        Task<GroupResponse> CreateGroup(CreateGroupRequest request);
        Task<GroupResponse> UpdateGroup(int id, UpdateGroupRequest request);
        Task DeleteGroup(int id);
    }
}

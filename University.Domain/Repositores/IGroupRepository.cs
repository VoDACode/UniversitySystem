using University.Domain.Entity.Group;

namespace University.Domain.Repositores
{
    public interface IGroupRepository
    {
        Task<bool> ExistsById(int id);
        Task<IQueryable<GroupEntity>> GetGroups();
        Task<GroupEntity?> GetGroupById(int id);
        Task<GroupEntity?> GetStudentsFromGroup(int id);
        Task<GroupEntity?> GetLessonsFromGroup(int id);
        Task<GroupEntity?> GetTasksFromGroup(int id);
        Task<GroupEntity?> GetCoursesFromGroup(int id);
        Task<GroupEntity> CreateGroup(GroupEntity request);
        Task<GroupEntity> UpdateGroup(GroupEntity request);
        Task DeleteGroup(int id);
    }
}

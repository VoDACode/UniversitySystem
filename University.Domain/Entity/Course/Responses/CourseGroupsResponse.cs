using University.Domain.Entity.Group.Responses;

namespace University.Domain.Entity.Course.Responses
{
    public class CourseGroupsResponse : CourseResponse
    {
        public IEnumerable<GroupResponse> Groups { get; set; } = new List<GroupResponse>();

        public CourseGroupsResponse(CourseEntity course) : base(course)
        {
            Groups = course.Groups.Select(g => new GroupResponse(g));
        }

        public CourseGroupsResponse()
        {
        }

        public static implicit operator CourseGroupsResponse(CourseEntity course)
        {
            return new CourseGroupsResponse(course);
        }
    }
}

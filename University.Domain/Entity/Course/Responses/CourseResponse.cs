namespace University.Domain.Entity.Course.Responses
{
    public class CourseResponse
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public DateOnly CreateAt { get; set; } = DateOnly.MinValue;

        public CourseResponse(CourseEntity course)
        {
            Id = course.Id;
            Name = course.Name;
            CreateAt = course.CreateAt;
        }

        public CourseResponse()
        {
        }

        public static implicit operator CourseResponse(CourseEntity course)
        {
            return new CourseResponse(course);
        }
    }
}

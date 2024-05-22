namespace University.Domain.Entity.Course.Responses
{
    public class CourseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateOnly CreateAt { get; set; }

        public CourseResponse(CourseEntity course)
        {
            Id = course.Id;
            Name = course.Name;
            CreateAt = course.CreateAt;
        }

        public static implicit operator CourseResponse(CourseEntity course)
        {
            return new CourseResponse(course);
        }
    }
}

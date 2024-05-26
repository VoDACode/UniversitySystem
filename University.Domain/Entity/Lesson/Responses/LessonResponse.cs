namespace University.Domain.Entity.Lesson.Responses
{
    public class LessonResponse
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int GroupId { get; set; } = 0;
        public int CourseId { get; set; } = 0;
        public int TeacherId { get; set; } = 0;
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.MinValue);
        public TimeSpan StartTime { get; set; } = new TimeSpan(0, 0, 0);
        public TimeSpan EndTime { get; set; } = new TimeSpan(23, 59, 59);

        public LessonResponse(LessonEntity lesson)
        {
            Id = lesson.Id;
            Name = lesson.Name;
            Description = lesson.Description ?? string.Empty;
            GroupId = lesson.GroupId;
            CourseId = lesson.CourseId;
            TeacherId = lesson.TeacherId;
            Date = lesson.Date;
            StartTime = lesson.StartTime;
            EndTime = lesson.EndTime;
        }

        public LessonResponse()
        {
        }

        public static implicit operator LessonResponse(LessonEntity lesson)
        {
            return new LessonResponse(lesson);
        }
    }
}

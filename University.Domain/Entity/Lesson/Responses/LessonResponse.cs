namespace University.Domain.Entity.Lesson.Responses
{
    public class LessonResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int GroupId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public DateOnly Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

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

        public static implicit operator LessonResponse(LessonEntity lesson)
        {
            return new LessonResponse(lesson);
        }
    }
}

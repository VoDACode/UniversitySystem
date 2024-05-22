namespace University.Domain.Entity.Task.Responses
{
    public class TaskResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CourseId { get; set; }
        public int GroupId { get; set; }
        public int TeacherId { get; set; }
        public DateTime? Deadline { get; set; }
        public int MaxScore { get; set; }
        public int MaxFiles { get; set; }
        public bool CanUpdate { get; set; }

        public TaskResponse(TaskEntity entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Content = entity.Content;
            CourseId = entity.CourseId;
            GroupId = entity.GroupId;
            TeacherId = entity.TeacherId;
            Deadline = entity.Deadline;
            MaxScore = entity.MaxMark;
            MaxFiles = entity.MaxFiles;
            CanUpdate = entity.CanUpdate;
        }

        public static implicit operator TaskResponse(TaskEntity entity)
        {
            return new TaskResponse(entity);
        }
    }
}

namespace University.Domain.Entity.Task.Responses
{
    public class TaskResponse
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int CourseId { get; set; } = 0;
        public int GroupId { get; set; } = 0;
        public int TeacherId { get; set; } = 0;
        public DateTime? Deadline { get; set; } = null;
        public int MaxScore { get; set; } = 0;
        public int MaxFiles { get; set; } = 0; 
        public bool CanUpdate { get; set; } = false;

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

        public TaskResponse()
        {
        }

        public static implicit operator TaskResponse(TaskEntity entity)
        {
            return new TaskResponse(entity);
        }
    }
}

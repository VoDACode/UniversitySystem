using University.Domain.Entity.File.Responses;
using University.Domain.Entity.User;

namespace University.Domain.Entity.TaskAnswer.Responses
{
    public class TaskAnswerResponse
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int StudentId { get; set; }
        public int? Mark { get; set; }
        public DateTime? MarkedAt { get; set; }
        public bool IsMarked => Mark.HasValue;
        public int? TeacherId { get; set; }
        public UserEntity? Teacher { get; set; }
        public IEnumerable<FileResponse> Files { get; set; }

        public TaskAnswerResponse(TaskAnswerEntity taskAnswer)
        {
            Id = taskAnswer.Id;
            TaskId = taskAnswer.TaskId;
            StudentId = taskAnswer.StudentId;
            Mark = taskAnswer.Mark;
            MarkedAt = taskAnswer.MarkedAt;
            TeacherId = taskAnswer.TeacherId;
            Teacher = taskAnswer.Teacher;
            Files = taskAnswer.Files.Select(file => new FileResponse(file));
        }

        public static implicit operator TaskAnswerResponse(TaskAnswerEntity taskAnswer)
        {
            return new TaskAnswerResponse(taskAnswer);
        }
    }
}

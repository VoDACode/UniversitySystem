namespace University.Domain.Entity.TaskAnswer.Requests
{
    public class CreateTaskAnswerRequest
    {
        public int TaskId { get; set; }
        public int StudentId { get; set; }
    }
}

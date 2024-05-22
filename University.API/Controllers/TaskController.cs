using Microsoft.AspNetCore.Mvc;
using University.Domain.Entity.Task.Requests;
using University.Domain.Entity.Task.Responses;
using University.Domain.Entity.TaskAnswer.Responses;
using University.Domain.Responses;
using University.Domain.Services;

namespace University.API.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : BaseController
    {
        protected readonly ITaskService _taskService;

        public TaskController(ILogger<TaskController> logger, ITaskService taskService) : base(logger)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IList<TaskResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> Get()
        {
            return await ExecuteAsync(_taskService.GetTasks);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<TaskResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> Get(int id)
        {
            return await ExecuteAsync(() => _taskService.GetTask(id));
        }

        [HttpGet("{id}/answers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IList<TaskAnswerResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> GetAnswers(int id)
        {
            return await ExecuteAsync(() => _taskService.GetTaskAnswersFromTask(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<TaskResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> Post([FromBody] CreateTaskRequest request)
        {
            return await ExecuteAsync(() => _taskService.CreateTask(request));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<TaskResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateTaskRequest request)
        {
            return await ExecuteAsync(() => _taskService.UpdateTask(id, request));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> Delete(int id)
        {
            return await ExecuteAsync(() => _taskService.DeleteTask(id));
        }
    }
}

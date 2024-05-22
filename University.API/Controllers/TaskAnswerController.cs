using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Domain.Entity.TaskAnswer.Requests;
using University.Domain.Entity.TaskAnswer.Responses;
using University.Domain.Responses;
using University.Domain.Services;

namespace University.API.Controllers
{
    [Route("api/task/answer")]
    [ApiController]
    public class TaskAnswerController : BaseController
    {
        protected readonly ITaskAnswerService _taskAnswerService;

        public TaskAnswerController(ILogger<TaskAnswerController> logger, ITaskAnswerService taskAnswerService) : base(logger)
        {
            _taskAnswerService = taskAnswerService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<TaskAnswerResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> Get(int id)
        {
            return await ExecuteAsync(() => _taskAnswerService.GetTaskAnswer(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<TaskAnswerResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> Post([FromQuery] CreateTaskAnswerRequest request, [FromBody] IFormFileCollection fileCollection)
        {
            return await ExecuteAsync(() => _taskAnswerService.CreateTaskAnswer(request, fileCollection));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<TaskAnswerResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> Put(int id, [FromQuery] UpdateTaskAnswerRequest request, [FromBody] IFormFileCollection fileCollection)
        {
            return await ExecuteAsync(() => _taskAnswerService.UpdateTaskAnswer(id, request, fileCollection));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> Delete(int id)
        {
            return await ExecuteAsync(() => _taskAnswerService.DeleteTaskAnswer(id));
        }

        [HttpPut("{id}/evaluate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<TaskAnswerResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> Evaluate(int id, [FromBody] EvaluateTaskAnswerRequest request)
        {
            return await ExecuteAsync(() => _taskAnswerService.EvaluateTaskAnswer(id, request));
        }
    }
}

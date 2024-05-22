using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Domain.Entity.Lesson.Requests;
using University.Domain.Entity.Lesson.Responses;
using University.Domain.Requests;
using University.Domain.Responses;
using University.Domain.Services;

namespace University.API.Controllers
{
    [Route("api/lesson")]
    [ApiController]
    public class LessonController : BaseController
    {
        protected readonly ILessonService _lessonService;

        public LessonController(ILogger<LessonController> logger, ILessonService lessonService) : base(logger)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IEnumerable<LessonResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> GetLessons([FromQuery] PageRequest request)
        {
            return await ExecuteAsync(async () => await _lessonService.GetLessons(request));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<LessonResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> GetLesson(int id)
        {
            return await ExecuteAsync(() => _lessonService.GetLesson(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BaseResponse<LessonResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> CreateLesson([FromBody] CreateLessonRequest request)
        {
            return await ExecuteAsync(() => _lessonService.CreateLesson(request));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<LessonResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> UpdateLesson(int id, [FromBody] UpdateLessonRequest request)
        {
            return await ExecuteAsync(() => _lessonService.UpdateLesson(id, request));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            return await ExecuteAsync(() => _lessonService.DeleteLesson(id));
        }
    }
}

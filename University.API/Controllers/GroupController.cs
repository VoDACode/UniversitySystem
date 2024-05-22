using Microsoft.AspNetCore.Mvc;
using University.Domain.Entity.Course.Responses;
using University.Domain.Entity.Group.Requests;
using University.Domain.Entity.Group.Responses;
using University.Domain.Entity.Lesson.Responses;
using University.Domain.Entity.Task.Responses;
using University.Domain.Entity.User.Responses;
using University.Domain.Responses;
using University.Domain.Services;

namespace University.API.Controllers
{
    [Route("api/group")]
    [ApiController]
    public class GroupController : BaseController
    {
        protected readonly IGroupService _groupService;

        public GroupController(ILogger<GroupController> logger, IGroupService groupService) : base(logger)
        {
            _groupService = groupService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IList<GroupResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> GetGroups()
        {
            return await ExecuteAsync(_groupService.GetGroups);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<GroupResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> GetGroup(int id)
        {
            return await ExecuteAsync(() => _groupService.GetGroup(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<GroupResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest request)
        {
            return await ExecuteAsync(() => _groupService.CreateGroup(request));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<GroupResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] UpdateGroupRequest request)
        {
            return await ExecuteAsync(() => _groupService.UpdateGroup(id, request));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            return await ExecuteAsync(() => _groupService.DeleteGroup(id));
        }

        [HttpGet("{id}/students")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IList<UserResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> GetStudentsFromGroup(int id)
        {
            return await ExecuteAsync(() => _groupService.GetStudentsFromGroup(id));
        }

        [HttpGet("{id}/lessons")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IList<LessonResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> GetLessonsFromGroup(int id)
        {
            return await ExecuteAsync(() => _groupService.GetLessonsFromGroup(id));
        }

        [HttpGet("{id}/tasks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IList<TaskResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> GetTasksFromGroup(int id)
        {
            return await ExecuteAsync(() => _groupService.GetTasksFromGroup(id));
        }

        [HttpGet("{id}/courses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IList<CourseResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> GetCoursesFromGroup(int id)
        {
            return await ExecuteAsync(() => _groupService.GetCoursesFromGroup(id));
        }
    }
}

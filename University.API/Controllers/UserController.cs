using Microsoft.AspNetCore.Mvc;
using University.Domain.Services;
using University.Domain.Entity.User.Responses;
using University.Domain.Responses;
using University.Domain.Requests;
using University.Domain.Entity.User.Requests;

namespace University.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {
        protected readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService) : base(logger)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<UserResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            return await ExecuteAsync(async () =>await _userService.GetUserById(id));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<PageResponse<UserResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> GetUsers([FromQuery] PageRequest request)
        {
            return await ExecuteAsync(async () => await _userService.GetUsers(request));
        }

        [HttpGet("{id}/detailed")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<DetailedUserResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> GetDetailedUserById([FromRoute] int id)
        {
            return await ExecuteAsync(async () => await _userService.GetDetailedUserById(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<UserResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            return await ExecuteAsync(async () => await _userService.CreateUser(request));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<UserResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserRequest request)
        {
            return await ExecuteAsync(async () => await _userService.UpdateUser(id, request));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<>))]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            return await ExecuteAsync(async () => { await _userService.DeleteUser(id); return new BaseResponse<object>(true); });
        }
    }
}

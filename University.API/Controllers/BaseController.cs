using Microsoft.AspNetCore.Mvc;
using University.Domain.Exceptions;
using University.Domain.Responses;

namespace University.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        protected async Task<IActionResult> ExecuteAsync(Func<Task> action)
        {
            return await ExecuteAsync(async () =>
            {
                await action();
                return "";
            });
        }

        protected async Task<IActionResult> ExecuteAsync<T>(Func<Task<T>> action) where T : class
        {
            try
            {
                var result = await action();
                return Ok(new BaseResponse<T>(true, null, result));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new BaseResponse<T>(false, ex.Message));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new BaseResponse<T>(false, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                _logger.LogDebug(ex.Message);
                return StatusCode(500, new BaseResponse<T>(false, "Internal server error"));
            }
        }
    }
}

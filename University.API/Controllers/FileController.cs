using Microsoft.AspNetCore.Mvc;
using University.Domain.Exceptions;
using University.Domain.Responses;
using University.Domain.Services;

namespace University.API.Controllers
{
    [Route("api/file")]
    public class FileController : BaseController
    {
        protected readonly IFileService _fileService;

        public FileController(ILogger<FileController> logger, IFileService fileService) : base(logger)
        {
            _fileService = fileService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<object>))]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var fileStreamResult = await _fileService.DownloadFile(id);
                return fileStreamResult;
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new BaseResponse<object>(false, ex.Message));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new BaseResponse<object>(false, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                _logger.LogDebug(ex.Message);
                return StatusCode(500, new BaseResponse<object>(false, "Internal server error"));
            }
        }
    }
}

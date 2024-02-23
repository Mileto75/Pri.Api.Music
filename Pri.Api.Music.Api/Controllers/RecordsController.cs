using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pri.Api.Music.Api.Dtos;
using Pri.Api.Music.Api.Extensions;
using Pri.CleanArchitecture.Music.Core.Interfaces.Services;

namespace Pri.Api.Music.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordService _recordService;

        public RecordsController(IRecordService recordService)
        {
            _recordService = recordService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _recordService.GetAllAsync();
            if (result.IsSucces)
            {
                return Ok(result.Value.MapToDto());
            }
            return BadRequest(result.Errors.ToArray());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _recordService.GetByIdAsync(id);
            if (result.IsSucces)
            {
                return Ok(result.Value.MapToDto());
            }
            return NotFound(result.Errors);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pri.Api.Music.Api.Dtos;
using Pri.Api.Music.Api.Extensions;
using Pri.CleanArchitecture.Music.Core.Interfaces.Services;
using Pri.CleanArchitecture.Music.Core.Services.Models;
using System.Xml.Linq;

namespace Pri.Api.Music.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordService _recordService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RecordsController(IRecordService recordService, IWebHostEnvironment webHostEnvironment)
        {
            _recordService = recordService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _recordService.GetAllAsync();
            return Ok(result.Value.MapToDto());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //get the record
            var result = await _recordService.GetByIdAsync(id);
            //check for errors
            if (result.IsSucces)
            {
                return Ok(result.Value.MapToDto());
            }
            return NotFound(result.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RecordRequestDto recordRequestDto)
        {
            var result = await _recordService.CreateRecordAsync(
                new RecordCreateRequestModel
                {
                    Title = recordRequestDto.Title,
                    Price = recordRequestDto.Price,
                    GenreId = recordRequestDto.GenreId,
                    ArtistId = recordRequestDto.ArtistId,
                    PropertyIds = recordRequestDto.PropertyIds,
                });
            if (result.IsSucces)
            {
                return CreatedAtAction(nameof(Get), new {ID = result.Value.Id },result.Value
                    .MapToDto());
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return BadRequest(ModelState.Values);
        }
        [HttpPut]
        public IActionResult Update(int id)
        {
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
        //implement SearchByArtist
        [HttpGet("Search/ByArtistName/{name}")]
        public async Task<IActionResult> SearchByArtist(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("character not allowed!");
            }
            var result = await _recordService.SearchByArtistAsync(name);
            if(result.IsSucces)
            {
                return Ok(result.Value.MapToDto());
                
            }
            return Ok(result.Errors);
        }
        [HttpGet("ByGenre/{id}")]
        public async Task<IActionResult> GetByGenreId(int id)
        {
            var result = await _recordService.GetRecordsByGenreIdAsync(id);
            if (result.IsSucces)
            {
                return Ok(result.Value.MapToDto());
            }
            return Ok(result.Errors);
        }
        [HttpGet("Search/ByTitle/{title}")]
        public async Task<IActionResult> SearchByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("character not allowed!");
            }
            var result = await _recordService.SearchByTitleAsync(title);
            if (result.IsSucces)
            {
                return Ok(result.Value.MapToDto());
            }
            return Ok(result.Errors);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Pri.CleanArchitecture.Music.Core.Interfaces.Services;
using Pri.CleanArchitecture.Music.Core.Services.Models;
using Pri.CleanArchitecture.Music.Web.ViewModels;

namespace Pri.CleanArchitecture.Music.Web.Controllers
{
    public class RecordsController : Controller
    {
        private readonly IRecordService _recordService;

        public RecordsController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _recordService.GetAllAsync();
            if(result.IsSucces)
            {
                //set the viewmodel
                var recordsIndexViewModel = new RecordsIndexViewModel
                {
                    Records = result.Value.Select(r =>
                    new RecordsDetailViewModel
                    {
                        Id = r.Id,
                        Title = r.Title,
                        Artist = r.Artist.Name,
                        Genre = r.Genre.Name,
                    })
                };
                return View(recordsIndexViewModel);
            }
            return View("Error",result.Errors);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _recordService.GetByIdAsync(id);
            if(result.IsSucces)
            {
                var recordsDetailViewModel = new RecordsDetailViewModel
                {
                    Id = result.Value.Id,
                    Title = result.Value.Title,
                    Artist = result.Value.Artist.Name,
                    Genre = result.Value.Genre.Name,
                };
                return View(recordsDetailViewModel);
            }
            Response.StatusCode = 404;
            return View("Error", result.Errors);
        }
        public async Task<IActionResult> Create()
        {
            var result = await _recordService.CreateRecordAsync
                (
                    new RecordCreateRequestModel 
                    {
                        Title = "Live in Brugge",
                        ArtistId = 22,
                        GenreId = 2,
                        Price = 99.23M,
                    }
                );
            if(result.IsSucces)
            {
                return RedirectToAction("Index");
            }
            return View("Error", result.Errors);
        }
        public async Task<IActionResult> Update(int id)
        {
            var updateRequestModel = new RecordUpdateRequestModel
            {
                Id = id,
                Title = "Live in Lapscheure",
                GenreId = 2,
                ArtistId = 2,
                Price = 23.33M
            };
            var result = await _recordService.UpdateRecordAsync(updateRequestModel);
            if(result.IsSucces)
            {
                return RedirectToAction("Index");
            }
            return View("Error", result.Errors);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pri.Api.Music.Core.Interfaces.Services;
using Pri.Api.Music.Web.ViewModels;
using Pri.CleanArchitecture.Music.Core.Interfaces.Services;
using Pri.CleanArchitecture.Music.Core.Services.Models;


namespace Pri.CleanArchitecture.Music.Web.Controllers
{
    public class RecordsController : Controller
    {
        private readonly IRecordService _recordService;
        private readonly IGenreService _genreService;
        private readonly IPropertyService _propertyService;
        private readonly IArtistService _artistService;

        public RecordsController(IRecordService recordService, IPropertyService propertyService, IGenreService genreService, IArtistService artistService)
        {
            _recordService = recordService;
            _propertyService = propertyService;
            _genreService = genreService;
            _artistService = artistService;
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
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var recordsCreateViewModel = new RecordsCreateViewModel();
            var properties = await _propertyService.GetAllAsync();
            var genres = await _genreService.GetAllAsync();
            var artists = await _artistService.GetAllAsync();
            recordsCreateViewModel.Properties = properties.Value
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                });
            recordsCreateViewModel.Genres = genres.Value.Select
                (p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                });
            recordsCreateViewModel.Artists = artists.Value.Select
                (
                   a => new SelectListItem 
                   {
                        Value = a.Id.ToString(),
                        Text = a.Name
                   }
                );
            return View(recordsCreateViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RecordsCreateViewModel recordsCreateViewModel)
        {
            var result = await _recordService.CreateRecordAsync(
                new RecordCreateRequestModel
                {
                    ArtistId = recordsCreateViewModel.ArtistId,
                    GenreId = recordsCreateViewModel.GenreId,
                    PropertyIds = recordsCreateViewModel.PropertyIds,
                    Title = recordsCreateViewModel.Title,
                    Price = recordsCreateViewModel.Price
                }
                );
            if (result.IsSucces)
                return RedirectToAction("Index");
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

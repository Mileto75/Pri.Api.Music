using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pri.Api.Music.Web.ViewModels
{
    public class RecordsCreateViewModel
    {
        public string Title { get; set; }
        public IEnumerable<SelectListItem> Genres { get; set; }
        public IEnumerable<SelectListItem> Artists { get; set; }
        public IEnumerable<SelectListItem> Properties { get; set; }
        public int GenreId { get; set; }
        public int ArtistId { get; set; }
        public IEnumerable<int> PropertyIds { get; set; }
        public decimal Price { get; set; }
    }
}
namespace Pri.Api.Music.Api.Dtos
{
    public class RecordRequestWithImageDto : RecordRequestDto
    {
        public IFormFile Image { get; set; }
    }
}

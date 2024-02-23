namespace Pri.Api.Music.Api.Dtos
{
    public class RecordsDetailResponseDto : BaseDto
    {
        public BaseDto Genre { get; set; }
        public BaseDto Artist { get; set; }
        public IEnumerable<BaseDto> Properties { get; set; }
    }
}

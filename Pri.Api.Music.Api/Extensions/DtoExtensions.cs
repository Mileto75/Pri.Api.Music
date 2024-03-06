using Pri.Api.Music.Api.Dtos;
using Pri.CleanArchitecture.Music.Core.Entities;

namespace Pri.Api.Music.Api.Extensions
{
    public static class DtoExtensions
    {
        public static RecordsResponseDto MapToDto(this IEnumerable<Record> records)
        {
            return new RecordsResponseDto
            {
                Records = records.Select(r => new BaseDto
                {
                    Id = r.Id,
                    Name = r.Title
                })
            };
        }
        public static RecordResponseDto MapToDto(this Record record)
        {
            return new RecordResponseDto
            {
                Id = record.Id,
                Name = record.Title,
                Price = record.Price,
                Genre = new BaseDto
                {
                    Id = record.Genre.Id,
                    Name = record.Genre.Name
                },
                Artist = new BaseDto
                {
                    Id = record.Artist.Id,
                    Name = record.Artist.Name
                },
                Properties
                = record.Properties.Select
                (p => new BaseDto
                {
                    Id = p.Id,
                    Name = p.Name
                }),
            };
        }
    }
}

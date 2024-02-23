using Microsoft.CodeAnalysis.CSharp.Syntax;
using Pri.Api.Music.Api.Dtos;
using Pri.CleanArchitecture.Music.Core.Entities;
using System.Xml.Linq;

namespace Pri.Api.Music.Api.Extensions
{
    public static class DtoExtensions
    {
        public static RecordsDetailResponseDto MapToDto(this Record toMap)
        {
            return new RecordsDetailResponseDto
            {
                Id = toMap.Id,
                Name = toMap.Title,
                Properties = toMap.Properties.Select(p => new BaseDto
                {
                    Id = p.Id,
                    Name = p.Name
                }),
                Artist = new BaseDto { Id = toMap.Artist.Id, Name = toMap.Artist.Name },
                Genre = new BaseDto { Id = toMap.Genre.Id, Name = toMap.Genre.Name },
            };
        }
        public static RecordsResponseDto MapToDto(this IEnumerable<Record> toMap)
        {
            return new RecordsResponseDto
            {
                Records = toMap.Select(r => new RecordsDetailResponseDto
                {
                    Id = r.Id,
                    Name = r.Title,
                    Properties = r.Properties.Select(p => new BaseDto
                    {
                        Id = p.Id,
                        Name = p.Name
                    }),
                    Artist = new BaseDto { Id = r.Artist.Id, Name = r.Artist.Name },
                    Genre = new BaseDto { Id = r.Genre.Id, Name = r.Genre.Name },
                })
            };
        }
        public static void MapToDto(this RecordsDetailResponseDto dto,Record record)
        {
            dto.Id = record.Id;
            dto.Name = record.Title;
            dto.Properties = record.Properties.Select(p => new BaseDto
            {
                Id = p.Id,
                Name = p.Name
            });
            dto.Artist = new BaseDto { Id = record.Artist.Id, Name = record.Artist.Name };
            dto.Genre = new BaseDto { Id = record.Genre.Id, Name = record.Genre.Name };
        }
    }
}

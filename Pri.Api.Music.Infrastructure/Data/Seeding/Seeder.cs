using Microsoft.EntityFrameworkCore;
using Pri.CleanArchitecture.Music.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Music.Infrastructure.Data.Seeding
{
    public static class Seeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            //Genres
            var genres = new Genre[]
            {
                new Genre {Id = 1, Name = "Metal"},
                new Genre {Id = 2, Name = "Blues"},
                new Genre {Id = 3, Name = "Grunge"},
            };
            //Artists
            var artists = new Artist[]
            {
                new Artist{Id = 1,Name="Walter Capiau Band"},
                new Artist{Id = 2,Name="Bart Soete and the hot bananas"},
                new Artist{Id = 3,Name="Pearl jam"},
            };
            //properties
            var properties = new Property[]
            {
                new Property{Id = 1,Name="Extended version" },
                new Property{Id = 2,Name="Limited edition" },
                new Property{Id = 3,Name="Live version" },
                new Property{Id = 4,Name="Explicit lyrics" },
            };
            //records
            var records = new Record[]
            {
                new Record{Id = 1, Title="Live in Lapscheure",GenreId=2,ArtistId=2,Price=99.00M},
                new Record{Id = 2, Title="Think about the children",GenreId=1,ArtistId=1,Price=9.00M},
                new Record{Id = 3, Title="Ten",GenreId=3,ArtistId=3,Price=299.00M},
            };
            //PropertyRecord
            var propertyRecords = new[]
            {
                new {PropertiesId = 1, RecordsId=1 },
                new {PropertiesId = 1, RecordsId=2 },
                new {PropertiesId = 1, RecordsId=3 },
                new {PropertiesId = 2, RecordsId=1 },
                new {PropertiesId = 2, RecordsId=2 },
                new {PropertiesId = 2, RecordsId=3 },
                new {PropertiesId = 3, RecordsId=1 },
                new {PropertiesId = 3, RecordsId=2 },
            };
            //hasdata methods
            modelBuilder.Entity<Genre>().HasData(genres);
            modelBuilder.Entity<Artist>().HasData(artists);
            modelBuilder.Entity<Property>().HasData(properties);
            modelBuilder.Entity<Record>().HasData(records);
            modelBuilder.Entity($"{nameof(Property)}{nameof(Record)}").HasData(propertyRecords);
        }
    }
}

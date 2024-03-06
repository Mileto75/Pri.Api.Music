using Microsoft.EntityFrameworkCore;
using Pri.Api.Music.Core.Interfaces.Repositories;
using Pri.Api.Music.Core.Interfaces.Services;
using Pri.Api.Music.Core.Services;
using Pri.Api.Music.Infrastructure.Repositories;
using Pri.CleanArchitecture.Music.Core.Interfaces.Repositories;
using Pri.CleanArchitecture.Music.Core.Interfaces.Services;
using Pri.CleanArchitecture.Music.Core.Services;
using Pri.CleanArchitecture.Music.Infrastructure.Data;
using Pri.CleanArchitecture.Music.Infrastructure.Repositories;

namespace Pri.Api.Music.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //Add database service
            builder.Services.AddDbContext<ApplicationDbContext>
                (options => options
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultDb")));
            // Add services to the container.
            builder.Services.AddScoped<IRecordRepository, RecordRepository>();
            builder.Services.AddScoped<IGenreRepository, GenreRepository>();
            builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
            builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
            builder.Services.AddScoped<IRecordService, RecordService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<IPropertyService, PropertyService>();
            builder.Services.AddScoped<IArtistService, ArtistService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

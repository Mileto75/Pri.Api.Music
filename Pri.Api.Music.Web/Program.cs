using Microsoft.EntityFrameworkCore;
using Pri.CleanArchitecture.Music.Core.Interfaces.Repositories;
using Pri.CleanArchitecture.Music.Core.Interfaces.Services;
using Pri.CleanArchitecture.Music.Core.Services;
using Pri.CleanArchitecture.Music.Infrastructure.Data;
using Pri.CleanArchitecture.Music.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
//Add database service
builder.Services.AddDbContext<ApplicationDbContext>
    (options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultDb")));
// Add services to the container.
builder.Services.AddScoped<IRecordRepository,RecordRepository>();
builder.Services.AddScoped<IGenreRepository,GenreRepository>();
builder.Services.AddScoped<IArtistRepository,ArtistRepository>();
builder.Services.AddScoped<IRecordService,RecordService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

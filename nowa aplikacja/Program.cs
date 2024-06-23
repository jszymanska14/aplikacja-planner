using Microsoft.EntityFrameworkCore;
using nowa_aplikacja.Models;
using nowa_aplikacja.Repositories;
using WeatherApi.Services;

var builder = WebApplication.CreateBuilder(args);
AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);

// obsługa kontrolerów i widoków
builder.Services.AddControllersWithViews();
// usługa HttpClient dla serwisu pogodowego
builder.Services.AddHttpClient<WeatherService>();
// Rejestracja DbContext
builder.Services.AddDbContext<TaskManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagerDatabase")));

// Rejestracja zależności dla ITaskRepository
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

var app = builder.Build();

// Konfiguracja potoku obsługi żądań HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); 
    
    app.UseHsts();
}

app.UseHttpsRedirection(); // Przekierowanie HTTP na HTTPS
app.UseStaticFiles(); // Obsługa plików statycznych

app.UseRouting(); // Routing

app.UseAuthorization(); // Autoryzacja

// Mapowanie domyślnej trasy dla kontrolerów
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Values}/{action=Index}/{id?}");

app.Run(); 

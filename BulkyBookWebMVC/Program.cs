using BulkyBookWebMVC.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
// Check if it's development or not in the environment; if it is, add the use developer exception
// page that will show user friendly exceptions, so they can be debugged and solved.
if (!app.Environment.IsDevelopment())
{   //If it's not development, then redirect to an error page
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// Middleware HTTPS redirection
app.UseHttpsRedirection();
// Middleware to use static files defined in wwwroot folder
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
// Map controller route that will map the patterns for MVC and redirect the request
// to the corresponding controllers and actions
app.MapControllerRoute(
    name: "default",
    // Inside the endpoints, we have a controller name and action name, and some ID
    // There's a pattern where we define a controller and an action method
    // Default, if nothing is provided, it should go to the home controller and it
    // should call the index action method
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

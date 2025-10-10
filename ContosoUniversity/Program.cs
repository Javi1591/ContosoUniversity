using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ContosoUniversity.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext") ?? throw new InvalidOperationException("Connection string 'SchoolContext' not found.")));

// Register service through dependency injection
builder.Services.AddScoped<ContosoUniversity.Services.IStudentProgressService, ContosoUniversity.Services.StudentProgressService>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// /healthz endpoint
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<SchoolContext>();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

// Map health check endpoint
app.MapHealthChecks("/healthz");

app.Run();

// For WebApplicationFactory<Program>
public partial class Program { }

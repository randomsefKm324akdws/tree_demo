using bl.NodesService.Models;
using da;
using da_ef_model;
using da.interfaces.ILogRepository;
using da.interfaces.INodesRepository;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(p => p.AddPolicy("cors-app", b =>
{
	b
		.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader()
		;
}));

builder.Services.AddScoped<AppDbContext, AppDbContext>();
builder.Services.AddScoped<INodesRepository, NodesRepository>();
builder.Services.AddScoped<INodesService, NodesService>();
builder.Services.AddScoped<ILogRepository, LogRepository>();




WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("cors-app");
app.UseAuthorization();
app.MapControllerRoute(
	"DefaultApi",
	"{controller=Home}/{action=Index}/{id?}"
);
app.MapControllers();

app.Run();
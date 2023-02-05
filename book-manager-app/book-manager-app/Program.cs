var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

var app = builder.Build();

app.UseMvc(route => route.MapRoute("Default", "{controller=Home}/{action=Search}"));

app.UseFileServer();

app.Run();
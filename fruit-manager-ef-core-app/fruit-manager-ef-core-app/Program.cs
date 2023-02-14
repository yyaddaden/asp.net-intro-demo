var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

var app = builder.Build();

app.UseMvc(route => route.MapRoute("Default", "{controller=Home}/{action=Index}"));

app.UseFileServer();

app.Run();

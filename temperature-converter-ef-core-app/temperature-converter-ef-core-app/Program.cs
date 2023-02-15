using temperature_converter_ef_core_app;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
builder.Services.AddDbContext<TemperatureConverterDbContext>();

var app = builder.Build();

app.UseMvc(route => route.MapRoute("Default", "{controller=Converter}/{action=ConvertOrAddUser}"));

app.UseFileServer();

app.Run();
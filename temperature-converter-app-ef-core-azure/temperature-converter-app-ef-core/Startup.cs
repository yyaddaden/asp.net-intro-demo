using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace temperature_converter_app_ef_core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddDbContext<Models.TemperatureConverterDbContext>(options =>
            {
                // options.UseSqlServer(Configuration["Data:ConnectionStrings:DefaultConnection"]);
                options.UseSqlServer(Configuration["Data:ConnectionStrings:AzureDbConnectionString"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseMvc(routes => routes.MapRoute("Converter", "{controller=Converter}/{action=ConvertOrAddUser}"));
        }
    }
}

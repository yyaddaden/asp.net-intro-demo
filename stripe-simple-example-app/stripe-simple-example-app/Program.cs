using stripe_simple_example_app;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

builder.Services.Configure<StripeSettings>(options =>
{
    options.PublicKey = builder.Configuration["Stripe:PublicKey"];
    options.SecretKey = builder.Configuration["Stripe:SecretKey"];
});

var app = builder.Build();

Stripe.StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

app.UseMvc(routes => routes.MapRoute("Default", "{controller=Stripe}/{action=Cart}"));

app.Run();
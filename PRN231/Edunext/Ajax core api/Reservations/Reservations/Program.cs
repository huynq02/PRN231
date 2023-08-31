using Microsoft.OpenApi.Models;
using Reservations.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
public void ConfigureServices(IServiceCollection services)
{

    services.AddSingleton<IRepository, Repository>();
    services.AddCors();
    services.AddControllersWithViews()
        .AddNewtonsoftJson()
        .AddxmlDataContractSerializerFormatters();

    services.AddControllers();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reservations", Version = "v1" });
    });
}
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reservations v1"));
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
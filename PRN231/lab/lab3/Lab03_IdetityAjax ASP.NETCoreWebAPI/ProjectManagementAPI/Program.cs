
using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProjectManagementAPI.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();//Allow CORS chú ý: Ajax phải đặt trên server
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<MyDbContext>(op
    => op.UseSqlServer(builder.Configuration.GetConnectionString("MyStoreDB")));
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
//chú ý: Ajax phải đặt trên server
builder.Services.AddSwaggerGen(
    c => 
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "ProjectManagementAPI",
            Version = "v1"
        });
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//chú ý: Ajax phải đặt trên server
app.UseCors(
    builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

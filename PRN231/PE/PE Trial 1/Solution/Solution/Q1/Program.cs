using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//var connectstring = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DBContext");
//builder.Services.AddDbContext<PE_PRN_Fall22B1Context>(opt =>
//{
//    opt.UseSqlServer(connectstring);
//});
//var mapperConfig = new MapperConfiguration(mc =>
//{
//    mc.AddProfile(new MappingProfile());
//});
//IMapper mapper = mapperConfig.CreateMapper();
//builder.Services.AddSingleton(mapper);
//builder.Services.AddCors(builder =>
//    builder.AddPolicy("corsapp", b => { b.WithOrigins("*").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); }));
//builder.Services.AddControllersWithViews().AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

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

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

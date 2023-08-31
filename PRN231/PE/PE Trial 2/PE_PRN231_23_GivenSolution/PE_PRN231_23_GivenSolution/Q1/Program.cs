using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Q1.AutoMapper;
using Q1.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var connectstring = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("PE_PRN231_Trial_02");
builder.Services.AddDbContext<PE_PRN231_Trial_02Context>(opt =>
{
    opt.UseSqlServer(connectstring);
});

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new DirectorsMapperProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
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



app.UseAuthorization();

app.MapControllers();

app.Run();




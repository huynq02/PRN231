using AutoMapper;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Q1_APIs.Models;
static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder modelBuilder = new();
    modelBuilder.EntitySet<Employee>("Employee");//Odata 
    return modelBuilder.GetEdmModel();
}
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddXmlSerializerFormatters();

var connectstring = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("PE_PRN_23Sum");
builder.Services.AddDbContext<PE_PRN_23SumContext>(opt =>
{
    opt.UseSqlServer(connectstring);
});
builder.Services.AddControllers().AddOData(option => option.AddRouteComponents("odata", GetEdmModel()).Select().Filter().Count().OrderBy().Expand().SetMaxTop(100));

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


app.MapControllers();

app.Run();

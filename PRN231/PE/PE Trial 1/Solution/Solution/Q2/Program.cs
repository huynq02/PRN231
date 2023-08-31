var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddCors();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
//app.UseCors(builder =>
//{
//    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
//});
//app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

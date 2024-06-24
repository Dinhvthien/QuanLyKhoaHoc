
using QuanLyKhoaHoc.Application.Common;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Services;
using QuanLyKhoaHoc.Application.Services.UserServices;
using QuanLyKhoaHoc.Infrastructure.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddScoped<IUser, CurrentUser>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddOpenApiDocument();
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


var app = builder.Build();


app.UseDefaultFiles();
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

   // var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

    //await initialiser.InitialiseAsync();

    //await initialiser.SeedAsync();

    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Services;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<ISubjectService, SubjectService>();

        return services;
    }
}
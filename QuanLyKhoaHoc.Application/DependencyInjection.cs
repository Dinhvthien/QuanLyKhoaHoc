using QuanLyKhoaHoc.Application.Common;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Services;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<ApplicationServiceBase<SubjectMapping, SubjectQuery, SubjectCreate, SubjectUpdate>, SubjectService>();

        services.AddScoped<ApplicationServiceBase<CourseMapping, CourseQuery, CourseCreate, CourseUpdate>, CourseService>();

        return services;
    }
}
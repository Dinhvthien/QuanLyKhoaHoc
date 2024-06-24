    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Domain.Entities;
using QuanLyKhoaHoc.Domain.Interfaces;
    using QuanLyKhoaHoc.Infrastructure.Data;
    using QuanLyKhoaHoc.Infrastructure.Repositories;

    namespace Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
    public static IServiceCollection AddInfrastructureServices(
   this IServiceCollection services,
   IConfiguration configuration
)
    {
        var connectionString = configuration.GetConnectionString(
            "DefaultConnection"
        );

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
        );

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IConfirmEmailRepository, ConfirmEmailRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }
}

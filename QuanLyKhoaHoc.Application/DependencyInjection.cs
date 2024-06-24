using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuanLyKhoaHoc.Application.Common;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common.Models;
using QuanLyKhoaHoc.Application.Services;
using QuanLyKhoaHoc.Application.Services.EmailServices;
using QuanLyKhoaHoc.Application.Services.JWTSerVices;
using QuanLyKhoaHoc.Application.Services.PermissionServices;
using QuanLyKhoaHoc.Application.Services.UserServices;
using QuanLyKhoaHoc.Domain.Interfaces;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthorization();
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                configuration["Jwt:SecretKey"]!
                            )
                        )
                    };
            });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<ApplicationServiceBase<SubjectMapping, SubjectQuery, SubjectCreate, SubjectUpdate>, SubjectService>();
        services.AddScoped<ApplicationServiceBase<CourseMapping, CourseQuery, CourseCreate, CourseUpdate>, CourseService>();
        services.AddScoped<ApplicationServiceBase<AdressMapping, AdressQuery, CreateAdress, UpdateAdress>, AdressService>();
        services.AddScoped<ApplicationServiceBase<SubjectDetailMapping, SubjectDetailQuery, SubjectDetailCreate, SubjectDetailUpdate>, SubjectDetailService>();
        services.AddScoped<ApplicationServiceBase<SubjectDetailMapping, SubjectDetailQuery, SubjectDetailCreate, SubjectDetailUpdate>, SubjectDetailService>();
        services.AddScoped<ApplicationServiceBase<CertificateTypeMapping, CertificateTypeQuery, CreateCertificateType, UpdateCertificateType>, CertificateTypeService>();
        services.AddScoped<ApplicationServiceBase<CertificateMapping, CertificateQuery, CreateCertificate, UpdateCertificate>, CertificateService>();


        services.AddScoped<IStatisticalService, StatisticalService>();
        services.AddScoped<IResponse, Response>();
        services.AddScoped<IUserService, UserGetService>();
        services.AddScoped<IConfirmEmailService, ConfirmEmailService>();
        services.AddScoped<ISendEmailService, SendEmailService>();
        services.AddScoped<IJwtAccessTokenService, JwtAccessTokenService>();
        services.AddScoped<IJwtRefreshTokenService, JwtRefreshTokenService>();
        services.AddScoped<ICreatePermissionService, CreatePermissionService>();
        return services;
    }
}
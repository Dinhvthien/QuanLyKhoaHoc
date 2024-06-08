using System.Security.Claims;

using QuanLyKhoaHoc.Application.Common.Interfaces;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    //public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? Id => "1";
    // truy vấn Certificateid ra cu ơi 
    public int? Certificateid => 1;
}

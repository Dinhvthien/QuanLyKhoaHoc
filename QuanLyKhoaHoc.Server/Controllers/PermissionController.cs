using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application.Common.Interfaces;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly ICreatePermissionService _createPermissionService;
        public PermissionController(ICreatePermissionService createPermissionService)
        {
                _createPermissionService = createPermissionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePermission(int userid, int roleid)
        {
            return Ok(await _createPermissionService.CreatePermission(userid, roleid));
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePermission(int userid, int roleid)
        {
            return Ok(await _createPermissionService.DeletePermission(userid, roleid));
        }
        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            return Ok(await _createPermissionService.GetPermission());
        }
        [HttpPut]   
        public async Task<IActionResult> UpdatePermission(int permissionid,int userid, int roleid)
        {
            return Ok(await _createPermissionService.UpdatePermission(permissionid,userid, roleid));
        }
    }
}

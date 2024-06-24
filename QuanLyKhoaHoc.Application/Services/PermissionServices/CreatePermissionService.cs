using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Domain.Entities;
using QuanLyKhoaHoc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Services.PermissionServices
{
    public class CreatePermissionService : ICreatePermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public CreatePermissionService(
            IPermissionRepository permissionRepository,
            IRoleRepository roleRepository,
            IUserRepository userRepository
        )
        {
            _permissionRepository = permissionRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> CreatePermission(int userId, int role)
        {
            try
            {

                if (userId == -1 || role == -1)
                {
                    return false;
                }
                var findUser = await _userRepository.FindByIdAsync(userId);
                if (findUser == null)
                {
                    return false;
                }
                var findPermission = await _permissionRepository.AnyAsync(c=>c.UserId == userId && c.RoleId == role);
                if (findPermission)
                {
                    return false;
                }
                var findRole = await _roleRepository.FindByIdAsync(role);
                if (findRole == null)
                {
                    return false;
                }
                await _permissionRepository.CreatePermission(userId, role);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeletePermission(int userId, int role)
        {
            try
            {
                var findPermission = await _permissionRepository.FindAsync(c => c.UserId == userId && c.RoleId == role);
                if (findPermission == null)
                {
                    return false;
                }
                await _permissionRepository.RemoveAsync(findPermission);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Permission>> GetPermission()
        {
            try
            {
                var a = await _permissionRepository.GetAllAsync();
                return a;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> NewDefaultPermission(int userId)
        {
            try
            {
                int roleId = await _roleRepository.FindRoleByName("student");

                if (roleId == -1)
                {
                    return false;
                }

                await _permissionRepository.CreatePermission(userId, roleId);
                return true;
            }
            catch
            {
                return false;
            }
        }
            
        public async Task<bool> UpdatePermission(int permissionid, int userId, int role)
        {
            try
            {
                var findPermission = await _permissionRepository.FindAsync(c => c.Id == permissionid);
                if (findPermission == null)
                {
                    return false;
                }
                findPermission.UserId = userId;
                findPermission.RoleId = role;
                await _permissionRepository.UpdateAsync(findPermission);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using QuanLyKhoaHoc.Application.Common.Models;
using QuanLyKhoaHoc.Application.Payload.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface IUserService
    {
        public Task<IResponse> GetInfo(int userId);
        public Task<IResponse> LoginAsUser(UserLoginRequest userLoginRequest);
        public Task<IResponse> ChangePassword(int userId, string newPassword);

        public Task<IResponse> ResetPassword(string userName);

        public Task<IResponse> ConfirmResetPassword(
            string userName,
            string codeResetPassword,
            string newPassword
        );
        public Task<IResponse> CreateUserRegister(UserRegisterRequest userRegisterRequest);
        public Task<IResponse> UpdateInfo(int userId, UserUpdateRequest userUpdateRequest);
        public Task<Result> LockUser(int userId);
        public Task<Result> UnLockUser(int userId);

    }
}

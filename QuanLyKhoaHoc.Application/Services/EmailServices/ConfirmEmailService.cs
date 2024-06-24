using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Models;
using QuanLyKhoaHoc.Application.Helper;
using QuanLyKhoaHoc.Domain.Entities;
using QuanLyKhoaHoc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Services.EmailServices
{
    public class ConfirmEmailService : IConfirmEmailService
    {
        private readonly IConfirmEmailRepository _repository;

        private readonly ICreatePermissionService _createPermissionService;

        public ConfirmEmailService(IConfirmEmailRepository repository, ICreatePermissionService createPermissionService)
        {
            _repository = repository;
            _createPermissionService = createPermissionService;
        }

        public ConfirmEmailService() { }
        public async Task<string> CreateConfirmEmail(int userId)
        {
            string confirmCode = RandomEmailConfirmCode.RandomCode(5);

            ConfirmEmail confirmEmail = new ConfirmEmail();
            confirmEmail.UserId = userId;
            confirmEmail.ConfirmCode = confirmCode;
            confirmEmail.ExpiryTime = DateTime.Now.AddMinutes(3);
            confirmEmail.IsConfirm = false;

            await _repository.AddAsync(confirmEmail);

            return confirmCode;
        }

        public async Task<Result> UserConfirmEmailUseCode(int userId, string confirmCode)
        {
            bool isConfirm = await _repository.ConfirmEmailUseCode(
              userId,
              confirmCode
          );

            if (isConfirm == false)
            {
                return new Result(Domain.ResultStatus.Failure, "Xác nhận email sai");
            }

            await _createPermissionService.NewDefaultPermission(userId);

            return new Result(Domain.ResultStatus.Succeess, "Xác nhận email thành công");
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Org.BouncyCastle.Crypto.Generators;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Models;
using QuanLyKhoaHoc.Application.Helper;
using QuanLyKhoaHoc.Application.Payload.Requests;
using QuanLyKhoaHoc.Application.Payload.Responses;
using QuanLyKhoaHoc.Application.Services.EmailServices;
using QuanLyKhoaHoc.Domain.Entities;
using QuanLyKhoaHoc.Domain.Interfaces;
using System.Net.Mail;
namespace QuanLyKhoaHoc.Application.Services.UserServices
{
    public class UserGetService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IResponse _response;
        private readonly IMapper _mapper;
        private readonly IJwtAccessTokenService _jwtAccessTokenService;
        private readonly IJwtRefreshTokenService _jwtRefreshTokenService;
        private readonly ISendEmailService _sendEmailService;
        private readonly IConfirmEmailRepository _confirmEmailRepository;
        private readonly IConfirmEmailService _confirmEmailService;
        private readonly ICreatePermissionService _createPermissionService;

        public UserGetService(IUserRepository userRepository, IResponse response, IMapper mapper,
            IJwtAccessTokenService jwtAccessTokenService,
            IJwtRefreshTokenService jwtRefreshTokenService,
            ISendEmailService sendEmailService,
            IConfirmEmailRepository confirmEmailRepository,
            IConfirmEmailService confirmEmailService,
            ICreatePermissionService createPermissionService

            )
        {
            _userRepository = userRepository;
            _response = response;
            _mapper = mapper;
            _sendEmailService = sendEmailService;
            _confirmEmailRepository = confirmEmailRepository;
            _jwtAccessTokenService = jwtAccessTokenService;
            _jwtRefreshTokenService = jwtRefreshTokenService;
            _confirmEmailService = confirmEmailService;
            _createPermissionService = createPermissionService;
        }

        public async Task<IResponse> ChangePassword(int userId, string newPassword)
        {
            User? user = await _userRepository.FindUser(userId);

            if (user == null)
            {
                return await _response.NoContent(
                    400,
                    "Không tìm thấy user"
                );
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdateAsync(user);

            return await _response.NoContent(
                200,
                "Thay đổi mật khẩu thành công"
            );
        }

        public async Task<IResponse> ConfirmResetPassword(string userName, string codeResetPassword, string newPassword)
        {
            User? user = await _userRepository.FindUser(userName);

            if (user == null)
            {
                return await _response.NoContent(
                    400,
                    "Không tìm thấy user"
                );
            }

            bool IsResetPassword =
                await _confirmEmailRepository.ConfirmEmailUseCode(
                    user.Id,
                    codeResetPassword
                );

            if (IsResetPassword == false)
            {
                return await _response.NoContent(
                    400,
                    "Không tìm thấy user"
                );
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdateAsync(user);

            return await _response.NoContent(
                     200,
                     "Thay đổi mật khẩu thành công"
                 );
        }
        public async Task<IResponse> CreateUserRegister(UserRegisterRequest userRegisterRequest)
        {
            User? user = await _userRepository.FindUser(userRegisterRequest.UserName);

            if (user == null)
            {
                User newUser = _mapper.Map<UserRegisterRequest, User>(
                    userRegisterRequest
                );
                newUser.CreateTime = DateTime.Now;
                newUser.UpdateTime = DateTime.Now;
                newUser.IsActive = false;
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

                try
                {
                    await _userRepository.CreateUser(newUser);
                    await _createPermissionService.CreatePermission(newUser.Id, 1);
                }
                catch
                {
                    return await _response.NoContent(
                         400,
                         "Có lỗi khi đăng ký tài khoản"
                     );
                }

                await _confirmEmailService.CreateConfirmEmail(newUser.Id);

                return await _response.Content(
                   201, "Đăng ký tài khoản thành công",
                    _mapper.Map<User, UserRegisterResponse>(newUser)
                );
            }

            if (user.IsActive)
            {
                return await _response.NoContent(
                    409,
                "Tài khoản đã tồn tại"
                );
            }

            string confirmCode = await _confirmEmailService.CreateConfirmEmail(
                user.Id
            );

            MimeMessage message = _sendEmailService.CreateMessage(
                user.Email,
                "Xác minh tài khoản",
                $"<b>Code:</b> {confirmCode}"
            );
            await _sendEmailService.Send(message);

            return await _response.Content(
                201,
              "Tạo tài khoản thành công",
                _mapper.Map<User, UserRegisterResponse>(user)
            );
        }

        public async Task<IResponse> GetInfo(int userId)
        {
            try
            {
                if (userId < 0 || userId == null)
                {
                    return await _response.NoContent(
                                   400,
                                  "Bạn cần phải đăng nhập"
                               );
                }
                User? user = await _userRepository.FindByIdAsync(userId);

                if (user == null)
                {
                    return await _response.NoContent(
                        400,
                       "Tài khoản đã tồn tại"
                    );
                }

                return await _response.Content(
                    200,
                    "Thành công",
                    _mapper.Map<User, UserGetResponse>(user));
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Result> LockUser(int userId)
        {
            try
            {
                if (userId == null && userId == 0)
                {
                    return new Result(Domain.ResultStatus.Failure, "Bạn cần nhập đủ thông tin");
                }

                User? user = await _userRepository.FindUser(userId);

                if (user == null)
                {
                    return new Result(Domain.ResultStatus.Failure, "Không tìm thấy user");
                }
                else
                {
                    user.UserStatus = Domain.UserStatus.Block;
                    await _userRepository.UpdateUser(user);
                    return new Result(Domain.ResultStatus.Succeess, "Khóa tài khoản thành công");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IResponse> LoginAsUser(UserLoginRequest userLoginRequest)
        {
            User? user = await _userRepository
             .Query(x => x.FullName == userLoginRequest.UserName)
             .Include(x => x.Permissions!)
             .ThenInclude(x => x.Role)
             .SingleOrDefaultAsync();

            if (user == null)
            {
                return await _response.NoContent(
                    401,
                    "Login không thành công"
                );
            }

            bool isUser = BCrypt.Net.BCrypt.Verify(userLoginRequest.Password, user.Password);

            if (isUser == false)
            {
                return await _response.NoContent(
                   401,
                   "Login không thành công"
               );
            }

            if (user.IsActive == false)
            {
                return await _response.NoContent(
                  401,
                  "Login không thành công"
              );
            }

            string accessToken = _jwtAccessTokenService.GenerateAccessToken(user);
            string refreshToken = _jwtRefreshTokenService.GenerateRefreshToken();

            await _jwtRefreshTokenService.LoginCreateRefreshToken(
                refreshToken,
                DateTime.Now.AddDays(10),
                user.Id
            );

            return await _response.Content(
               200,
               "Login thành công",
                new { AccessToken = accessToken, RefreshToken = refreshToken }
            );
        }

        public async Task<IResponse> ResetPassword(string userName)
        {
            User? user = await _userRepository.FindUser(userName);

            if (user == null)
            {
                return await _response.NoContent(
                   400,
                   "Không tìm thấy user"
               );
            }

            string code = RandomEmailConfirmCode.RandomCode(5);

            ConfirmEmail confirmEmail = new ConfirmEmail()
            {
                ConfirmCode = code,
                ExpiryTime = DateTime.Now.AddMinutes(3),
                UserId = user.Id,
                IsConfirm = false
            };
            await _confirmEmailRepository.AddAsync(confirmEmail);

            var message = _sendEmailService.CreateMessage(
                user.Email,
                "Reset password",
                $"<b>Code:</b> {code}"
            );
            await _sendEmailService.Send(message);

            return await _response.NoContent(
                  200,
                  "Thay đổi mật khẩu thành công"
            );
        }
        public async Task<Result> UnLockUser(int userId)
        {
            try
            {
                if (userId == null && userId == 0)
                {
                    return new Result(Domain.ResultStatus.Failure, "Bạn cần nhập đủ thông tin");
                }

                User? user = await _userRepository.FindUser(userId);

                if (user == null)
                {
                    return new Result(Domain.ResultStatus.Failure, "Không tìm thấy user");
                }
                else
                {
                    user.UserStatus = Domain.UserStatus.Active;
                    await _userRepository.UpdateUser(user);
                    return new Result(Domain.ResultStatus.Succeess, "Mở khóa tài khoản thành công");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IResponse> UpdateInfo(int userId, UserUpdateRequest userUpdateRequest)
        {
            User? user = await _userRepository.FindByIdAsync(userId);

            if (user == null)
            {
                return await _response.NoContent(
                    400,
                    "Không tìm thấy user"
                );
            }

            _mapper.Map(userUpdateRequest, user);

            await _userRepository.UpdateUser(user);

            return await _response.NoContent(
               200,
               "Update thành công"
            );
        }
    }
}

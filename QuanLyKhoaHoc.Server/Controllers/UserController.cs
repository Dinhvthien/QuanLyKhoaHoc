using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Payload.Requests;
using QuanLyKhoaHoc.Application.Services.UserServices;
using System.Security.Claims;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfirmEmailService _confirmEmailService;


        public UserController(
            IUserService userService,
            IConfirmEmailService confirmEmailService

        )
        {
            _userService = userService;
            _confirmEmailService = confirmEmailService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> UserRegister(
            UserRegisterRequest userRegisterRequest
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(
                await _userService.CreateUserRegister(userRegisterRequest)
            );
        }

        [HttpPost]
        [Route("confirm")]
        public async Task<IActionResult> UserConfirmEmail(
            UserConfirmEmailRequest userConfirmEmailRequest
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(
                await _confirmEmailService.UserConfirmEmailUseCode(
                    userConfirmEmailRequest.UserId,
                    userConfirmEmailRequest.ConfirmCode
                )
            );
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> UserLogin(
            UserLoginRequest userLoginRequest
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _userService.LoginAsUser(userLoginRequest));
        }
        [HttpPost]
        [Route("LockUser")]
        public async Task<IActionResult> LockUser(int Userid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _userService.LockUser(Userid));
        }
        [HttpPost]
        [Route("UnLockUser")]
        public async Task<IActionResult> UnLockUser(int Userid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _userService.UnLockUser(Userid));
        }
        [HttpPost]
        [Route("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(
            UserChangePasswordRequest userChangePasswordRequest
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return Ok(
                await _userService.ChangePassword(
                    userId,
                    userChangePasswordRequest.NewPassword
                )
            );
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(
            UserResetPasswordRequest userResetPasswordRequest
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(
                await _userService.ResetPassword(
                    userResetPasswordRequest.UserName
                )
            );
        }

        [HttpPost]
        [Route("confirm-reset-password")]
        public async Task<IActionResult> ConfirmResetPassword(
            UserConfirmResetPasswordRequest userConfirmResetPasswordRequest
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(
                await _userService.ConfirmResetPassword(
                    userConfirmResetPasswordRequest.UserName,
                    userConfirmResetPasswordRequest.ConfirmCode,
                    userConfirmResetPasswordRequest.NewPassword
                )
            );
        }

        [HttpPatch]
        [Route("update")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(
            UserUpdateRequest userUpdateRequest
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return Ok(
                await _userService.UpdateInfo(userId, userUpdateRequest)
            );
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            return Ok(await _userService.GetInfo(userId));
        }
    }
}

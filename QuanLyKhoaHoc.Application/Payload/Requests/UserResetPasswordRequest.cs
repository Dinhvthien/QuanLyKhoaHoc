using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Payload.Requests
{
    public class UserResetPasswordRequest
    {
        [Required(ErrorMessage = "UserName là bắt buộc.")]
        [MaxLength(50, ErrorMessage = "UserName không được quá 50 ký tự.")]
        public string UserName { get; set; } = null!;
    }
}

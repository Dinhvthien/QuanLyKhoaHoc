using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Payload.Requests
{
    public class UserChangePasswordRequest
    {
        [Required(ErrorMessage = "Password là bắt buộc.")]
        [MaxLength(100, ErrorMessage = "Password không được quá 100 ký tự.")]
        public string NewPassword { get; set; } = null!;
    }
}

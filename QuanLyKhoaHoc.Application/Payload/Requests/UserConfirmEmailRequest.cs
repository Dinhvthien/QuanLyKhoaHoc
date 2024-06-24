using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Payload.Requests
{
    public class UserConfirmEmailRequest
    {
        [Required(ErrorMessage = "UserId là bắt buộc.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "ConfirmCode là bắt buộc")]
        [MaxLength(5, ErrorMessage = "ConfirmCode chỉ có 5 ký tự.")]
        [MinLength(5, ErrorMessage = "ConfirmCode chỉ có 5 ký tự.")]
        public string ConfirmCode { get; set; } = null!;
    }
}

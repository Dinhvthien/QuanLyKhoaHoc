using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Payload.Responses
{
    public class UserGetResponse
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Avatar { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
    }
}

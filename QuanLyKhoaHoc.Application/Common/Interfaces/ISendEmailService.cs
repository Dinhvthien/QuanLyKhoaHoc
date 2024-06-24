using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface ISendEmailService
    {
        public MimeMessage CreateMessage(string toAddress,string subject,string htmlBody);

        public Task Send(MimeMessage message);
    }
}

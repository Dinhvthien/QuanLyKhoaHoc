using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Application.Services.EmailServices
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IConfiguration _configuration;

        public SendEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public MimeMessage CreateMessage(string toAddress, string subject, string htmlBody)
        {
            var email = new MimeMessage();
            email.From.Add(
                MailboxAddress.Parse(_configuration["Smtp:FromAddress"])
            );
            email.To.Add(MailboxAddress.Parse(toAddress));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = htmlBody };

            return email;
        }

        public async Task Send(MimeMessage message)
        {
            var smtp = new SmtpClient();
        smtp.Connect(
            _configuration["Smtp:Server"],
            int.Parse(_configuration["Smtp:Port"]!)
        );
        // smtp.Authenticate();
        await smtp.SendAsync(message);
        smtp.Disconnect(true);
        }
    }
}

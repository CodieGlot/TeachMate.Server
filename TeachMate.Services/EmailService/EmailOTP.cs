using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Domain;

namespace TeachMate.Services
{
    public class EmailOtp : IEmailOtp
    {
        private readonly NetworkCredential _credential;
        private readonly EmailConfig _emailConfig;
        private readonly DataContext _context;

        private OtpService _OtpService;

        public EmailOtp(IOptions<EmailConfig> emailConfig, DataContext context, OtpService otpService)

        {
            _emailConfig = emailConfig.Value;
            _credential = new NetworkCredential(_emailConfig.AppEmail, _emailConfig.AppPassword);
            _context = context;
            _OtpService = otpService;
        }
        public async Task<ResponseDto> SendEmailOtp(EmailReciveDto dto)
        {
            
            var appUser = await _context.AppUsers.FirstOrDefaultAsync(p => p.Email == dto.GmailReceiveOTP);

                        if (appUser == null)
            {
                throw new BadRequestException("Wrong Email");
            }
            else
            {
                Random rnd = new Random();

                int digit1 = rnd.Next(0, 10);
                int digit2 = rnd.Next(0, 10);
                int digit3 = rnd.Next(0, 10);
                int digit4 = rnd.Next(0, 10);

                // Concatenating the digits into a string
                string randomNumberString = $"{digit1}{digit2}{digit3}{digit4}";

                MailMessage message = new MailMessage();
                message.From = new MailAddress(_emailConfig.AppEmail);
                message.Subject = "OTP";
                message.To.Add(new MailAddress(dto.GmailReceiveOTP));
                message.IsBodyHtml = true;
                _OtpService.SetOtp(randomNumberString);
                message.Body = $"This is your OTP : {randomNumberString}";
                var otp = new UserOTP
                {
                    Gmail = dto.GmailReceiveOTP,
                    OTP = randomNumberString,
                };
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = _credential,
                    EnableSsl = true
                };
                
                await _context.UserOTPs.AddAsync(otp);
                _context.SaveChanges();
                smtpClient.Send(message);
                return new ResponseDto("Send success");
            }

        }




       



    }
}


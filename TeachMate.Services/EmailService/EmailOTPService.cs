using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using TeachMate.Domain;

namespace TeachMate.Services
{
    public class EmailOTPService : IEmailOtp
    {
        private readonly NetworkCredential _credential;
        private readonly EmailConfig _emailConfig;
        private readonly DataContext _context;


        public EmailOTPService(IOptions<EmailConfig> emailConfig, DataContext context)

        {
            _emailConfig = emailConfig.Value;
            _credential = new NetworkCredential(_emailConfig.AppEmail, _emailConfig.AppPassword);
            _context = context;
        }
        public async Task<ResponseDto> SendEmailOtp(EmailReciveDto dto)
        {

            var appUser = await _context.AppUsers.FirstOrDefaultAsync(p => p.Email == dto.Email);

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
                message.To.Add(new MailAddress(dto.Email));
                message.IsBodyHtml = true;
                message.Body = "<div style=\"font-family: Helvetica,Arial,sans-serif;min-width:1000px;overflow:auto;line-height:2\">\r\n " +
                    " <div style=\"margin:50px auto;width:70%;padding:20px 0\">\r\n    " +
                    "<div style=\"border-bottom:1px solid #eee\">\r\n     " +
                    " <a href=\"\" style=\"font-size:1.4em;color: #00466a;text-decoration:none;font-weight:600\">TeachMate</a>\r\n   " +
                    " </div>\r\n    <p style=\"font-size:1.1em\">Hi,</p>\r\n    " +
                    "<p>Thank you for choosing TeachMate. Use the following OTP to complete your procedures. OTP is valid for 2 minutes</p>\r\n   " +
                   $"<h2 style=\"background: #00466a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;\">{randomNumberString}</h2> " +
                    "<p style=\"font-size:0.9em;\">Regards,<br />TeachMate</p>\r\n   " +
                    " <hr style=\"border:none;border-top:1px solid #eee\" />\r\n  " +
                    "  <div style=\"float:right;padding:8px 0;color:#aaa;font-size:0.8em;line-height:1;font-weight:300\">\r\n     " +
                    " <p>TeachMate Inc</p>\r\n         </div>\r\n  </div>\r\n</div>";




                var otp = new UserOTP
                {
                    Gmail = dto.Email,
                    OTP = randomNumberString,
                };
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = _credential,
                    EnableSsl = true
                };

                var userOTP = _context.UserOTPs.FirstOrDefault(otp => otp.Gmail.Equals(dto.Email));

                if (userOTP != null)
                {
                    userOTP.OTP = randomNumberString;
                    userOTP.Gmail = dto.Email;
                    userOTP.CreateAt = DateTime.Now;
                    userOTP.ExpireAt = DateTime.Now.AddMinutes(3);
                }
                else if (userOTP == null)
                {
                    await _context.UserOTPs.AddAsync(otp);
                }
                 _context.Update(userOTP);
                await _context.SaveChangesAsync();
                smtpClient.Send(message);
                return new ResponseDto("Send success");
            }

        }








    }
}


using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TeachMate.Domain;

namespace TeachMate.Services
{

    public class UserDetailService : IUserDetailService
    {


        private readonly DataContext _context;


        public UserDetailService(DataContext context)
        {
            _context = context;
        }


        public async Task<AppUser> AddTutorDetail(AppUser user, AddTutorDetailDto dto)
        {
            user.DisplayName = dto.DisplayName;
            user.Tutor.DisplayName = dto.DisplayName;
            user.PhoneNumber = dto.PhoneNumber;
            user.Avatar = dto.Avatar;
            user.Tutor.Description = dto.Description;
            user.Tutor.GradeLevel = dto.GradeLevel;
            await _context.SaveChangesAsync();
            return user;

        }

        public async Task<AppUser> AddLearnerDetail(AppUser user, AddLearnerDetailDto dto)
        {
            user.DisplayName = dto.DisplayName;
            user.Learner.DisplayName = dto.DisplayName;
            user.PhoneNumber = dto.PhoneNumber;
            user.Avatar = dto.Avatar;
            user.Learner.GradeLevel = dto.GradeLevel;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<ResponseDto> UpdateLearnerDetail(AppUser user, UpdateLearnerDetailDto dto)
        {
            var user1 = await _context.AppUsers.Where(x => x.Email == dto.Email).FirstOrDefaultAsync();
            if (user1 != null && dto.Email != user.Email)
            {
                throw new BadRequestException("Email already register");
            }
            user.DisplayName = string.IsNullOrEmpty(dto.DisplayName) ? user.DisplayName : dto.DisplayName;
            user.Learner.DisplayName = string.IsNullOrEmpty(dto.DisplayName) ? user.Learner.DisplayName : dto.DisplayName;
            user.Email = string.IsNullOrEmpty(dto.Email) || !IsValidEmail(dto.Email) ? user.Email : dto.Email;
            user.PhoneNumber = string.IsNullOrEmpty(dto.PhoneNumber) ? user.PhoneNumber : dto.PhoneNumber;
            user.Avatar = string.IsNullOrEmpty(dto.Avatar) ? user.Avatar : dto.Avatar;
            user.Learner.GradeLevel = dto.GradeLevel == 0 ? user.Learner.GradeLevel : dto.GradeLevel;

            await _context.SaveChangesAsync();
            return new ResponseDto("Update Success");
        }
        public async Task<ResponseDto> UpdateTutorDetail(AppUser user, UpdateTutorDetailDto dto)
        {
            var user1 = await _context.AppUsers.Where(x => x.Email == dto.Email).FirstOrDefaultAsync();
            if (user1 != null && dto.Email != user.Email)
            {
                throw new BadRequestException("Email already register");
            }
            

            user.DisplayName = string.IsNullOrEmpty(dto.DisplayName) ? user.DisplayName : dto.DisplayName;
            user.Tutor.DisplayName = string.IsNullOrEmpty(dto.DisplayName) ? user.Tutor.DisplayName : dto.DisplayName;
            user.Email = string.IsNullOrEmpty(dto.Email) || !IsValidEmail(dto.Email) ? user.Email : dto.Email;
            user.PhoneNumber = string.IsNullOrEmpty(dto.PhoneNumber) ? user.PhoneNumber : dto.PhoneNumber;
            user.Avatar = string.IsNullOrEmpty(dto.Avatar) ? user.Avatar : dto.Avatar;
            user.Tutor.GradeLevel = dto.GradeLevel == 0 ? user.Tutor.GradeLevel : dto.GradeLevel;
            user.Tutor.Description = String.IsNullOrEmpty(dto.Description) ? user.Tutor.Description : dto.Description;
            await _context.SaveChangesAsync();
            return new ResponseDto("Update Success");

        }
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            // Kiểm tra xem có ký tự @ trong chuỗi email không
            int atIndex = email.IndexOf('@');
            if (atIndex == -1)
                return false;

            // Tách phần username và tên miền
            string username = email.Substring(0, atIndex);
            string domain = email.Substring(atIndex + 1);

            // Kiểm tra định dạng của phần username
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9._]+$"))
                return false;

            // Kiểm tra định dạng của phần domain
            if (!Regex.IsMatch(domain, @"^[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                return false;

            return true;
        }




    }





}

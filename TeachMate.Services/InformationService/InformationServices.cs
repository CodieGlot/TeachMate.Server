using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Domain;
using TeachMate.Domain.DTOs.InformationDto;

namespace TeachMate.Services
{
    
    public class InformationServices : IInformationServices
    {
        
       
        private readonly DataContext _context;


        public InformationServices(DataContext context)
        {
            _context = context;
        }

        
        public async Task<AppUser> AddTutorDetail(AppUser user, AddTutorDetailDto dto)
        {
            
            user.DisplayName = dto.DisplayName;
            user.Tutor.DisplayName=dto.DisplayName;
            user.Email = dto.Email;
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
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.Avatar = dto.Avatar;
            user.Learner.GradeLevel = dto.GradeLevel;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<AppUser> UpdateLearnerDetail(AppUser user, AddLearnerDetailDto dto)
        {
            user.DisplayName = string.IsNullOrEmpty(dto.DisplayName) ? user.DisplayName : dto.DisplayName;
            user.Learner.DisplayName = string.IsNullOrEmpty(dto.DisplayName) ? user.Learner.DisplayName : dto.DisplayName;
            user.Email = string.IsNullOrEmpty(dto.Email) ? user.Email : dto.Email;
            user.PhoneNumber = string.IsNullOrEmpty(dto.PhoneNumber) ? user.PhoneNumber : dto.PhoneNumber;
            user.Avatar = string.IsNullOrEmpty(dto.Avatar) ? user.Avatar : dto.Avatar;
            user.Learner.GradeLevel = dto.GradeLevel == 0 ? user.Learner.GradeLevel : dto.GradeLevel; 

            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<AppUser> UpdateTutorDetail(AppUser user, AddTutorDetailDto dto)
        {
            user.DisplayName = string.IsNullOrEmpty(dto.DisplayName) ? user.DisplayName : dto.DisplayName;
            user.Tutor.DisplayName = string.IsNullOrEmpty(dto.DisplayName) ? user.Tutor.DisplayName : dto.DisplayName;
            user.Email = string.IsNullOrEmpty(dto.Email) ? user.Email : dto.Email;
            user.PhoneNumber = string.IsNullOrEmpty(dto.PhoneNumber) ? user.PhoneNumber : dto.PhoneNumber;
            user.Avatar = string.IsNullOrEmpty(dto.Avatar) ? user.Avatar : dto.Avatar;
            user.Tutor.GradeLevel = dto.GradeLevel == 0 ? user.Tutor.GradeLevel : dto.GradeLevel;
            user.Tutor.Description =String.IsNullOrEmpty(dto.Description)? user.Tutor.Description : dto.Description;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<AppUser> ChangeUserPassWord(AppUser user, UserPassword dto)
        {
            if (!BCrypt.Net.BCrypt.Verify(dto.Old_Password, user.Password))
            {
                throw new UnauthorizedException();
            }
            else if (dto.Old_Password == dto.New_Password)
            {
                throw new InvalidOperationException("New password must be different from the old password.");
            }
            dto.New_Password = BCrypt.Net.BCrypt.HashPassword(dto.New_Password);
            user.Password = dto.New_Password;
            await _context.SaveChangesAsync();
            return user;
        }

      

        
    }

    
      

       
}

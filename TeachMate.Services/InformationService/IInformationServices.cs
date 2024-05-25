using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Domain;
using TeachMate.Domain.DTOs.InformationDto;

namespace TeachMate.Services
{
    public interface IInformationServices
    {
        Task<AppUser> AddTutorDetail(AppUser user, AddTutorDetailDto dto);
        Task<AppUser> AddLearnerDetail(AppUser user, AddLearnerDetailDto dto);
        Task<AppUser> UpdateLearnerDetail(AppUser user, AddLearnerDetailDto dto);
        Task<AppUser> UpdateTutorDetail(AppUser user, AddTutorDetailDto dto);
        Task<AppUser> ChangeUserPassWord (AppUser user, UserPassword dto);
    }
}

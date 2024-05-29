using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Domain;

namespace TeachMate.Services
{
    public interface IUserDetailService
    {
        Task<AppUser> AddTutorDetail(AppUser user, AddTutorDetailDto dto);
        Task<AppUser> AddLearnerDetail(AppUser user, AddLearnerDetailDto dto);
        Task<AppUser> UpdateLearnerDetail(AppUser user, AddLearnerDetailDto dto);
        Task<AppUser> UpdateTutorDetail(AppUser user, AddTutorDetailDto dto);
    }
}

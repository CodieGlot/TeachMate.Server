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
        Task<ResponseDto> UpdateLearnerDetail(AppUser user, UpdateLearnerDetailDto dto);
        Task<ResponseDto> UpdateTutorDetail(AppUser user, UpdateTutorDetailDto dto);
    }
}

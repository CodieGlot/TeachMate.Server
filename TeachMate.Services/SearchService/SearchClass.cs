using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachMate.Domain;

namespace TeachMate.Services
{
    public class SearchClass : ISearchClass
    {
        private readonly DataContext _context;

        public SearchClass(DataContext context)
        {
            _context = context;
        }

        public async Task<List<LearningModule>> Search(SearchClassDto dto)
        {
            var query = _context.LearningModules.AsQueryable();

            /*if (!string.IsNullOrWhiteSpace(dto.TitleOrDesc))
            {
                var trimmedSearchTerm = dto.TitleOrDesc.Trim().ToLower();
                query = query.Where(m => EF.Functions.Like(m.Title.ToLower(), $"%{trimmedSearchTerm}%") ||
                                         EF.Functions.Like(m.Description.ToLower(), $"%{trimmedSearchTerm}%"));
            }*/
            if (dto.TitleOrDesc != null)
            {
                query = query.Where(m => m.Title == dto.TitleOrDesc);
            }

            if (dto.Subject != null && dto.Subject != Subject.None)
            {
                query = query.Where(m => m.Subject == dto.Subject);
            }

            if (dto.GradeLevel != null) 
            {
                query = query.Where(m => m.GradeLevel == dto.GradeLevel);
            }

            if (dto.StartOpenDate != null && dto.EndOpenDate != null)
            {
                query = query.Where(m => m.StartDate >= dto.StartOpenDate && m.EndDate <= dto.EndOpenDate);
            }

            if (dto.MaximumLearners != null)
            {
                query = query.Where(m => m.MaximumLearners <= dto.MaximumLearners);
            }

            if (dto.ModuleType != null)
            {
                query = query.Where(m => m.ModuleType == dto.ModuleType);
            }

            if (dto.NumOfWeeks != null)
            {
                query = query.Where(m => m.NumOfWeeks == dto.NumOfWeeks);
            }

            return await query.ToListAsync();
        }
    }
}

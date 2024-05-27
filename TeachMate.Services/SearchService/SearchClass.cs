using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachMate.Domain;
using TeachMate.Domain.DTOs.SearchDto;

namespace TeachMate.Services.SearchService
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

            if (!string.IsNullOrWhiteSpace(dto.TitleOrDesc))
            {
                var trimmedSearchTerm = dto.TitleOrDesc.Trim().ToLower();
                query = query.Where(m => m.Title.ToLower().Contains(trimmedSearchTerm) || m.Description.ToLower().Contains(trimmedSearchTerm));
            }

            if (dto.Subject != Subject.None)
            {
                query = query.Where(m => m.Subject == dto.Subject);
            }

            if (dto.GradeLevel > -1) // Only apply filter if gradeLevel is provided
            {
                query = query.Where(m => m.GradeLevel == dto.GradeLevel);
            }

            if (dto.StartOpenDate != default(DateOnly) && dto.EndOpenDate != default(DateOnly))
            {
                query = query.Where(m => m.StartDate >= dto.StartOpenDate && m.EndDate <= dto.EndOpenDate);
            }

            if (dto.MaximumLearners > 0)
            {
                query = query.Where(m => m.MaximumLearners <= dto.MaximumLearners);
            }

            if (dto.ModuleType != ModuleType.Weekly)
            {
                query = query.Where(m => m.ModuleType == dto.ModuleType);
            }

            if (dto.NumOfWeeks > 0)
            {
                query = query.Where(m => m.NumOfWeeks == dto.NumOfWeeks);
            }

            return await query.ToListAsync();
        }
    }
}

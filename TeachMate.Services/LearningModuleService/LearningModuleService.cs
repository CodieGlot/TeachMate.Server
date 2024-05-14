using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TeachMate.Domain;

namespace TeachMate.Services;
public class LearningModuleService : ILearningModuleService
{
    private readonly DataContext _context;

    public LearningModuleService(DataContext context)
    {
        _context = context;
    }
    public async Task<LearningModule?> GetLearningModuleById(int id)
    {
        return await _context.LearningModules
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<List<LearningModule>> GetLearningModulesOfTutor(Guid tutorId)
    {
        return await _context.LearningModules
            .Where(x => x.TutorId == tutorId)
            .ToListAsync();
    }
    public async Task<LearningModule> CreateLearningModule(AppUser user, CreateLearningModuleDto dto)
    {
        var learningModule = new LearningModule
        {
            Title = dto.Title,
            Description = dto.Description,
            Subject = dto.Subject,
            Duration = dto.Duration,
            CreatedAt = dto.CreatedAt,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            MaximumLearners = dto.MaximumLearners,
        };

        learningModule.Schedule = JsonSerializer.Serialize(dto.Schedule);

        if (user.Tutor != null)
        {
            user.Tutor.CreatedModules.Add(learningModule);
        }

        _context.Update(user);
        await _context.SaveChangesAsync();

        return learningModule;
    }
}

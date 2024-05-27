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
        var learningModule = await _context.LearningModules
            .Include(x => x.EnrolledLearners)
            .Include(x => x.WeeklySchedule)
            .FirstOrDefaultAsync(x => x.Id == id);

        /*if (learningModule != null)
        {
            learningModule.Schedule = JsonSerializer.Deserialize<List<LearningSession>>(learningModule.SerializedSchedule) ?? new List<LearningSession>();
        } */

        return learningModule;
    }
    public async Task<List<LearningModule>> GetAllCreatedModules(AppUser user)
    {
        if (user.Tutor != null)
        {
            await _context.Entry(user.Tutor)
                .Collection(x => x.CreatedModules)
                .LoadAsync();
        }
        return user.Tutor?.CreatedModules ?? new List<LearningModule>();
    }
    public async Task<List<LearningModule>> GetAllEnrolledModules(AppUser user)
    {
        if (user.Learner != null)
        {
            await _context.Entry(user.Learner)
                .Collection(x => x.EnrolledModules)
                .LoadAsync();
        }

        return user.Learner?.EnrolledModules ?? new List<LearningModule>();
    }
    public async Task<LearningModule> EnrollLearningModule(AppUser user, int moduleId)
    {
        var learningModule = await _context.LearningModules
            .FirstOrDefaultAsync(x => x.Id == moduleId);

        if (learningModule == null)
        {
            throw new BadRequestException("Module does not exist.");
        }

        if (user.Learner != null)
        {
            learningModule.EnrolledLearners.Add(user.Learner);
        }

        _context.Update(learningModule);
        await _context.SaveChangesAsync();

        return learningModule;
    }
    public async Task<LearningModule> CreateLearningModule(AppUser user, CreateLearningModuleDto dto)
    {

        var learningModule = new LearningModule
        {
            Title = dto.Title,
            Description = dto.Description,
            Subject = dto.Subject,
            GradeLevel = dto.GradeLevel,
            Duration = dto.Duration,
            CreatedAt = dto.CreatedAt,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            MaximumLearners = dto.MaximumLearners,
            ModuleType = dto.ModuleType,
            //WeeklySchedule = dto.WeeklySchedule,
            NumOfWeeks = dto.NumOfWeeks,
        };

        if (dto.ModuleType == ModuleType.Custom)
        {
            learningModule.WeeklySchedule = null;
        }

        else if (dto.ModuleType == ModuleType.Weekly)
        {
            var listWeeklySlotDto = dto.WeeklySlots;
            var listWeeklySlot = new List<WeeklySlot>();
            learningModule.WeeklySchedule = new WeeklySchedule();
            for (int i = 0; i < listWeeklySlotDto.Count; i++)
            {
                var weeklySlot = new WeeklySlot()
                {
                    DayOfWeek = listWeeklySlotDto[i].DayOfWeek,
                    StartTime = listWeeklySlotDto[i].StartTime,
                    EndTime = listWeeklySlotDto[i].EndTime,
                    
                };
                listWeeklySlot.Add(weeklySlot);
            }
            learningModule.WeeklySchedule.NumberOfSlot = listWeeklySlot.Count();
            learningModule.WeeklySchedule.WeeklySlots = listWeeklySlot;
        }

        if (user.Tutor != null)
        {
            user.Tutor.CreatedModules.Add(learningModule);
        }
        
        _context.Update(user);
        await _context.SaveChangesAsync();

        return learningModule;
    }
    public async Task<List<LearningModuleRequest>> GetAllCreatedRequests(Guid requesterId)
    {
        return await _context.LearningModuleRequests
            .Where(x => x.RequesterId == requesterId)
            .ToListAsync();
    }
    public async Task<List<LearningModuleRequest>> GetAllReceivedRequests(Guid tutorId)
    {
        return await _context.LearningModuleRequests
            .Where(x => x.LearningModule.TutorId == tutorId)
            .ToListAsync();
    }
    public async Task<LearningModuleRequest?> GetRequestById(int id)
    {
        var request = await _context.LearningModuleRequests
            .FirstOrDefaultAsync(x => x.Id == id);

        /*if (request != null)
        {
            request.Schedule = JsonSerializer.Deserialize<List<LearningSession>>(request.SerializedSchedule) ?? new List<LearningSession>();
        }*/

        return request;
    }
    public async Task<LearningModuleRequest> CreateLearningModuleRequest(AppUser user, CreateLearningModuleRequestDto dto)
    {
        var request = new LearningModuleRequest
        {
            RequesterId = user.Id,
            RequesterDisplayName = user.DisplayName,
            LearningModuleId = dto.LearningModuleId,
            Title = dto.Title,
            
            Status = RequestStatus.Waiting,
            
        };

        _context.LearningModuleRequests.Add(request);
        await _context.SaveChangesAsync();

        return request;
    }
    public async Task<LearningModuleRequest> UpdateRequestStatus(int requestId, UpdateRequestStatusDto dto)
    {
        var request = await _context.LearningModuleRequests
            .FirstOrDefaultAsync(x => x.Id == requestId);

        if (request == null)
        {
            throw new BadRequestException("Request does not exist.");
        }

        request.Status = dto.Status;

        _context.Update(request);
        await _context.SaveChangesAsync();

        return request;
    }
    public async Task<List<LearningModuleRequest>> GetAllReceivedRequests(int moduleId, Guid tutorId)
    {
        return await _context.LearningModuleRequests
           .Where(x => x.LearningModuleId == moduleId && x.LearningModule.TutorId == tutorId)
           .ToListAsync();
    }
}

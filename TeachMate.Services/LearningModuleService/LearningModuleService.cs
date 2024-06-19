using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TeachMate.Domain;


namespace TeachMate.Services;
public class LearningModuleService : ILearningModuleService
{
    private readonly DataContext _context;
    private readonly IUserService _userService;

    public LearningModuleService(DataContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }
    public async Task<LearningModule?> GetLearningModuleById(int id)
    {
        
        var learningModule = await _context.LearningModules
            .Include(x => x.EnrolledLearners)
            .Include(x => x.Schedule)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (learningModule.ModuleType == ModuleType.Weekly)
        {
            var learningModuleWeekly = await _context.LearningModules
            .Include(x => x.EnrolledLearners)
            .Include(x => x.Schedule)
            .Include(x => x.WeeklySchedule)
            .ThenInclude(x => x.WeeklySlots)
            .FirstOrDefaultAsync(x => x.Id == id);
            return learningModuleWeekly;
        }
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
    public async Task<LearningModule> EnrollLearningModule(Guid learnerId, int moduleId)
    {
        var learningModule = await GetLearningModuleById(moduleId);

        var learner = await _userService.GetUserById(learnerId);
        if (learner == null || learner.Learner == null)
        {
            throw new BadRequestException("Learner does not exist.");
        }
        if (learningModule == null)
        {
            throw new BadRequestException("Module does not exist.");
        }

        learner.Learner.EnrolledModules.Add(learningModule);
        _context.Update(learner);
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
            EndDate = dto.StartDate.AddDays(7 * dto.NumOfWeeks),
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
            learningModule.WeeklySchedule = new WeeklySchedule() ;
            /*var listWeeklySlotDto = dto.WeeklySlots;
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
            learningModule.WeeklySchedule.WeeklySlots = listWeeklySlot;*/
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
        var learningModule = await GetLearningModuleById(dto.LearningModuleId);
        if (learningModule == null)
        {
            throw new BadRequestException("Module does not exist.");
        }
        var requestOld = _context.LearningModuleRequests.Where(x => x.LearningModuleId == dto.LearningModuleId && x.RequesterId == user.Id).FirstOrDefault();
        if (requestOld != null)
        {
            throw new BadRequestException("You have already requested this class.");

        }
        var request = new LearningModuleRequest
        {
            RequesterId = user.Id,
            RequesterDisplayName = user.DisplayName,
            LearningModuleId = dto.LearningModuleId,
            Title = dto.Title,
            
            Status = RequestStatus.Waiting,
            
        };

        if (learningModule.MaximumLearners <= learningModule.EnrolledLearners.Count)
        {
            throw new Exception("The Classes is full");
        }
        _context.LearningModuleRequests.Add(request);
        await _context.SaveChangesAsync();

        return request;
    }

    public async Task<LearningModuleRequest> UpdateRequestStatus(UpdateRequestStatusDto dto)
    {
        var learningModule = await GetLearningModuleById(dto.LearningModuleId);
        if (learningModule == null)
        {
            throw new BadRequestException("Module does not exist.");
        }
        var request = await _context.LearningModuleRequests
            .FirstOrDefaultAsync(x => x.Id == dto.LearningRequestId);

        if (request == null)
        {
            throw new BadRequestException("Request does not exist.");
        }
        
        request.Status = dto.Status;

        _context.Update(request);

        //if (request.Status == RequestStatus.Rejected)
        //{
        //    learningModule.LearningModuleRequests.Remove(request);
        //}else
        if (learningModule.MaximumLearners <= learningModule.EnrolledLearners.Count)
        {
            throw new Exception("Class is full");
        }else
        if (request.Status == RequestStatus.Approved)
        {
            await EnrollLearningModule(request.RequesterId, request.LearningModuleId);
            //learningModule.LearningModuleRequests.Remove(request);
        }
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

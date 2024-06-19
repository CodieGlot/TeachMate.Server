using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TeachMate.Domain;
using TeachMate.Domain.DTOs.ScheduleDto;

namespace TeachMate.Services;

public class ScheduleService : IScheduleService
{
    private readonly DataContext _context;
    private readonly ILearningModuleService _learningModuleService;

    private readonly IUserService _userService;
    public ScheduleService(DataContext context, ILearningModuleService learningModuleService, IUserService userService)
    {
        _context = context;
        _learningModuleService = learningModuleService;
        _userService = userService;
    }

    public async Task<LearningModule> AddWeeklySchedule(AddWeeklyScheduleDto dto)
    {
        var learningModule = await _learningModuleService.GetLearningModuleById(dto.LearningModuleId);
        var listWeeklySlotDto = dto.WeeklySlots;
        var listWeeklySlot = new List<WeeklySlot>();
        if (learningModule == null) throw new NotFoundException("Learning module not found");
        learningModule.WeeklySchedule = new WeeklySchedule();
        if (learningModule.ModuleType == ModuleType.Custom)
        {
            throw new BadRequestException("This learning module type is custom");
        }
        for (int i = 0; i < listWeeklySlotDto.Count; i++)
        {
            var weeklySlot = new WeeklySlot()
            {
                DayOfWeek = listWeeklySlotDto[i].DayOfWeek,
                StartTime = listWeeklySlotDto[i].StartTime,

            };
            listWeeklySlot.Add(weeklySlot);
        }
        learningModule.WeeklySchedule.NumberOfSlot = listWeeklySlot.Count();
        learningModule.WeeklySchedule.WeeklySlots = listWeeklySlot;
        _context.Update(learningModule);
        await _context.SaveChangesAsync();
        await UpdateWeeklyLearningSession(learningModule.Id);
        return learningModule;
    }

    public async Task<LearningSession> CreateCustomLearningSession(CreateCustomLearningSessionDto dto, AppUser user)
    {
       
        LearningModule learningModule = await _learningModuleService.GetLearningModuleById(dto.LearningModuleId);
        if (learningModule == null) throw new NotFoundException("Can not found learning module");
        if (learningModule.ModuleType == ModuleType.Weekly)
        {
            throw new BadRequestException("This learning module type is weekly");
        }
        LearningSession session = new LearningSession()
        {
            Date = dto.Date,
            StartTime = dto.StartTime,
            EndTime = dto.StartTime.AddMinutes(learningModule.Duration),
            LinkMeet = "...",
            Title = dto.Title,
            LearningModule = learningModule
        };
        if (await CheckDuplicateLearningSession(session, user))
        {
            throw new BadRequestException("Unable to schedule session. The selected time slot overlaps with an existing session in your schedule. Please choose a different time or consult your schedule for availability.");
        }
        var count = await _context.LearningSessions.Where(x => x.LearningModuleId == learningModule.Id).CountAsync();
        session.Slot = count + 1;
        learningModule.Schedule.Add(session);
        _context.Update(learningModule);
        await _context.SaveChangesAsync();
        return session;
    }

    public async Task<LearningModule> UpdateWeeklyLearningSession(int id)
    {
        var learningModule = await _learningModuleService.GetLearningModuleById(id);
        if (learningModule == null) throw new NotFoundException("Not found learning module");
        if (learningModule.ModuleType == ModuleType.Custom) throw new BadRequestException("The module type is custom");
        if (!learningModule.Schedule.IsNullOrEmpty()) throw new BadRequestException("Learning Module already update sessions");
        if (learningModule.WeeklySchedule.WeeklySlots.IsNullOrEmpty()) throw new BadRequestException("Weekly Slots has not been defined");
        
        var weeklySlots = learningModule.WeeklySchedule.WeeklySlots;
        var numOfWeeks = learningModule.NumOfWeeks;
        var startDate = learningModule.StartDate;
        DayOfWeek startDayOfWeek = startDate.DayOfWeek;
        DayOfWeek closestDay = startDayOfWeek;
        //Find the date which close to startDate
        var weeklySlotsOrdered = weeklySlots.OrderBy(x => ((int)x.DayOfWeek - (int)startDayOfWeek + 7) % 7).ToList();
        //First week
        var learningSessions = new List<LearningSession>();
        int slotCount = 0;
        DateOnly dateMark = startDate;
        for (int j = 0; j < numOfWeeks; j++)
        {
            for (int i = 0; i < weeklySlotsOrdered.Count; i++)
            {
                var learningSession = new LearningSession
                {
                    Date = dateMark.AddDays(((int)weeklySlotsOrdered[i].DayOfWeek - (int)startDayOfWeek + 7) % 7),
                    StartTime = weeklySlotsOrdered[i].StartTime,
                    EndTime = weeklySlotsOrdered[i].StartTime.AddMinutes(learningModule.Duration),
                    Slot = ++slotCount,
                    Title = learningModule.Title + " Slot " + slotCount,
                    LinkMeet = "...",

                };
                learningSessions.Add(learningSession);
            }
            dateMark = dateMark.AddDays(7);
        }
        learningModule.Schedule = learningSessions;
        _context.Update(learningModule);
        await _context.SaveChangesAsync();
        return learningModule;

    }
    public async Task<List<LearningSession>> GetScheduleById(int id)
    {
        var learningSessions =await _context.LearningModules
            .Where(u => u.Id == id)
            .Select(u => u.Schedule).FirstAsync();
        foreach (var learningSession in learningSessions)
        {
            learningSession.LearningModuleName = (await _learningModuleService.GetLearningModuleById(learningSession.LearningModuleId)).Title;
        }
        return learningSessions;
    }

    public async Task<LearningSession> UpdateLearningSession(CreateCustomLearningSessionDto dto, AppUser user)
    {
        var learningSession = await _context.LearningSessions.Where(u => u.Id == dto.Id).FirstOrDefaultAsync();
        if (learningSession == null)
        {
            throw new Exception("Custom learning session not found");
        }

        var tempLearningSession = new LearningSession
        {
            Id = learningSession.Id,
            Title = string.IsNullOrEmpty(dto.Title) ? learningSession.Title : dto.Title,
            Date = dto.Date != default(DateOnly) ? dto.Date : learningSession.Date,
            StartTime = dto.StartTime != default(TimeOnly) ? dto.StartTime : learningSession.StartTime,
            LinkMeet = string.IsNullOrEmpty(dto.LinkMeet) ? learningSession.LinkMeet : dto.LinkMeet,
            LearningModuleId = learningSession.LearningModuleId
        };

        var tempDate = learningSession.Date;
        learningSession.Date = DateOnly.MinValue;
        await _context.SaveChangesAsync();

        if (await CheckDuplicateLearningSession(tempLearningSession, user))
        {
            learningSession.Date = tempDate;
            await _context.SaveChangesAsync();
            throw new BadRequestException("Unable to schedule session. The selected time slot overlaps with an existing session in your schedule. Please choose a different time or consult your schedule for availability.");
        }
        learningSession.Title = tempLearningSession.Title;
        learningSession.Date = tempLearningSession.Date;
        learningSession.StartTime = tempLearningSession.StartTime;
        learningSession.LinkMeet = tempLearningSession.LinkMeet;
        await _context.SaveChangesAsync();
        return learningSession;
    }

    public async Task<LearningSession> DeleteLearningSessionById(int id)
    {
        var learningSession = await _context.LearningSessions
                                            .Where(u => u.Id == id)
                                            .FirstOrDefaultAsync();

        if (learningSession == null)
        {
            throw new Exception("Custom learning session not found.");
        }

        _context.LearningSessions.Remove(learningSession);
        await _context.SaveChangesAsync();
        return learningSession;
    }
    public async Task<List<LearningSession>> GetScheduleByTutor(AppUser tutor)
    {
        var learningSessions = await _context.LearningSessions
            .Where(ls => ls.LearningModule.TutorId == tutor.Id) 
            .OrderBy(ls => ls.Date) 
            .ThenBy(ls => ls.StartTime) 
            .Select(s => new LearningSession
            {
                Id = s.Id,
                Slot = s.Slot,
                Title = s.Title,
                Date = s.Date,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                LinkMeet = s.LinkMeet,
                LearningModuleId = s.LearningModuleId,
                  LearningModuleName = s.LearningModule.Title
            })
            .ToListAsync();

        return learningSessions;
    }

    public async Task<List<LearningSession>> GetScheduleByLearner(AppUser learner)
    {
        var learningModules = await _learningModuleService.GetAllEnrolledModules(learner);
        var learningSessions = new List<LearningSession>();
        var list = new List<LearningSession>();

        foreach (var learningModule in learningModules)
        {
           list  = await _context.LearningSessions
                .Where(x => x.LearningModule.Id == learningModule.Id)
                .OrderBy(ls => ls.Date)
                .ThenBy(ls => ls.StartTime)
                 .Select(s => new LearningSession
                 {
                     Id = s.Id,
                     Slot = s.Slot,
                     Title = s.Title,
                     Date = s.Date,
                     StartTime = s.StartTime,
                     EndTime = s.EndTime,
                     LinkMeet = s.LinkMeet,
                     LearningModuleId = s.LearningModuleId,
                     LearningModuleName = s.LearningModule.Title
                     
                 }).ToListAsync();
            learningSessions.AddRange(list);
        }
         
        return learningSessions;
    }

    public async Task<bool> CheckDuplicateLearningSession(LearningSession newSession, AppUser user)
    {
        var learningSessions = new List<LearningSession>();
        if (user.Tutor != null)
        {
            learningSessions = await GetScheduleByTutor(user);
        }
        else if (user.Learner != null)
        {
            learningSessions = await GetScheduleByLearner(user);
        };
        return learningSessions.Any(x => x.Date == newSession.Date && (((newSession.StartTime <= x.EndTime) && (newSession.StartTime >= x.StartTime)) 
        || ((newSession.EndTime >= x.StartTime) && (newSession.EndTime <= x.EndTime))));
       
    }

    public async Task<LearningSession> GetLearningSessionById(int id)
    {
        var learningSession = await _context.LearningSessions.Where(x => x.Id == id).Include(x => x.LearningModule).FirstOrDefaultAsync();

        if (learningSession == null) throw new NotFoundException("Learning Session not found");
        return learningSession;
    }

    public async Task<LearningSession> CreateFreeLearningSession(CreateCustomLearningSessionDto dto, AppUser user)
    {
        LearningModule learningModule = await _learningModuleService.GetLearningModuleById(dto.LearningModuleId);
        if (learningModule == null) throw new NotFoundException("Can not found learning module");
        if (dto.Date >= learningModule.StartDate) throw new BadRequestException("Free learning session must be scheduled before the start date of learning module");

        LearningSession session = new LearningSession()
        {
            Date = dto.Date,
            StartTime = dto.StartTime,
            EndTime = dto.StartTime.AddMinutes(learningModule.Duration),
            LinkMeet = dto.LinkMeet,
            Title = dto.Title,
            LearningModule = learningModule
        };
        if (await CheckDuplicateLearningSession(session, user))
        {
            throw new BadRequestException("Unable to schedule this free session. The selected time slot overlaps with an existing session in your schedule. Please choose a different time or consult your schedule for availability.");
        }
        var count = await _context.LearningSessions.Where(x => x.LearningModuleId == learningModule.Id).CountAsync();
        session.Slot = 0;
        learningModule.Schedule.Add(session);
        _context.Update(learningModule);
        await _context.SaveChangesAsync();
        return session;
    }

    

}

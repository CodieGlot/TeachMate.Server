using Microsoft.IdentityModel.Tokens;
using TeachMate.Domain;
using TeachMate.Domain.DTOs.ScheduleDto;

namespace TeachMate.Services;

public class ScheduleService : IScheduleService
{
    private readonly DataContext _context;
    private readonly ILearningModuleService _learningModuleService;
    public ScheduleService(DataContext context, ILearningModuleService learningModuleService)
    {
        _context = context;
        _learningModuleService = learningModuleService;

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
                EndTime = listWeeklySlotDto[i].EndTime,

            };
            listWeeklySlot.Add(weeklySlot);
        }
        learningModule.WeeklySchedule.NumberOfSlot = listWeeklySlot.Count();
        learningModule.WeeklySchedule.WeeklySlots = listWeeklySlot;
        _context.Update(learningModule);
        await _context.SaveChangesAsync();
        return learningModule;
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
                    EndTime = weeklySlotsOrdered[i].EndTime,
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
    public Task<LearningSession> CreateCustomLearningSession()
    {
        throw new NotImplementedException();
    }

}

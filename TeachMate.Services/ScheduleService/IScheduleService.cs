using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Domain;
using TeachMate.Domain.DTOs.ScheduleDto;

namespace TeachMate.Services;

public interface IScheduleService
{
    Task<LearningModule> AddWeeklySchedule(AddWeeklyScheduleDto dto);
    Task<LearningModule> UpdateWeeklyLearningSession(int id);
    Task<LearningSession> CreateCustomLearningSession(CreateCustomLearningDto dto, AppUser user);
    Task<List<LearningSession>> GetScheduleById(int id);

    Task<List<LearningSession>> GetScheduleByTutor(AppUser tutor);
    Task<List<LearningSession>> GetScheduleByLearner(AppUser learner);
    Task<bool> CheckDuplicateLearningSession(LearningSession newSession, AppUser user);
}

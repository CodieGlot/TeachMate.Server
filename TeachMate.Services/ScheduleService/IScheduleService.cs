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
    Task<LearningModule> AddWeeklySchedule(AddWeeklyScheduleDto dto, AppUser user);
    Task<LearningModule> UpdateWeeklyLearningSession(int id, AppUser user);
    Task<LearningSession> CreateCustomLearningSession(CreateCustomLearningSessionDto dto, AppUser user);
    Task<List<LearningSession>> GetScheduleById(int id);

    Task<LearningSession> UpdateLearningSession(CreateCustomLearningSessionDto dto, AppUser user);
    Task<LearningSession> DeleteLearningSessionById(int id);

    Task<List<LearningSession>> GetScheduleByTutor(AppUser tutor);
    Task<List<LearningSession>> GetScheduleByLearner(AppUser learner);
    Task<bool> CheckDuplicateLearningSession(LearningSession newSession, AppUser user);

    Task<LearningSession> GetLearningSessionById(int id);

    Task<LearningSession> CreateFreeLearningSession(CreateCustomLearningSessionDto dto, AppUser user);
}

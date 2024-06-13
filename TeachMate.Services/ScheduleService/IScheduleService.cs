﻿using System;
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
    Task<LearningSession> CreateCustomLearningSession(CreateCustomLearningDto dto);
    Task<List<LearningSession>> GetScheduleById(int id);
    Task<LearningSession> UpdateCustomLearning(CreateCustomLearningDto dto);
    Task<LearningSession> DeleteCustomLearningById(int id);
}

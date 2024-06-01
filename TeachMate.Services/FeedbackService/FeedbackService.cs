using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Domain;
using TeachMate.Services;
using Microsoft.EntityFrameworkCore;

namespace TeachMate.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly DataContext _context;
        private readonly ILearningModuleService _learningModuleService;
        private readonly IUserService _appUserService;

        public FeedbackService(DataContext context, ILearningModuleService learningModuleService, IUserService appUserService)
        {
            _context = context;
            _learningModuleService = learningModuleService;
            _appUserService = appUserService;
        }

        public async Task<LearningModuleFeedback> AddFeedback(LearnerFeedbackDto dto, AppUser appUser)
        {
            try
            {
                var learningModule = await _learningModuleService.GetLearningModuleById(dto.LearningModuleId);
                if (learningModule == null)
                {
                    throw new InvalidOperationException($"LearningModule with ID {dto.LearningModuleId} not found.");
                }

                var feedback = new LearningModuleFeedback
                {
                    Comment = dto.Comment,
                    Star = dto.Star,
                    LearningModule = learningModule,
                    AppUser = appUser,
                    IsAnonymous = dto.IsAnonymous,
                };

                _context.LearningModuleFeedbacks.Add(feedback);
                await _context.SaveChangesAsync();

                return feedback;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while saving feedback: {ex.Message}");
            }
        }

        public async Task<LearningModule> GetLearningModuleById(int moduleId)
        {
            return await _context.LearningModules.FindAsync(moduleId);
        }
    }
}
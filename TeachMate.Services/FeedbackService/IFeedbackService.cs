using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Domain;

namespace TeachMate.Services
{
    public interface IFeedbackService
    {
        Task<LearningModule> GetLearningModuleById(int moduleId);
        Task<LearningModuleFeedback?> AddFeedback(LearnerFeedbackDto dto, AppUser appUser);
    }
}

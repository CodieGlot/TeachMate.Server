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
        Task<LearningModuleFeedback?> AddFeedback(LearnerFeedbackDto dto, AppUser appUser);
        Task<LearningModuleFeedback> LikeFeedback(int feedbackId, AppUser appUser);
        Task<LearningModuleFeedback> DisLikeFeedback(int feedbackId, AppUser appUser);

    }
}

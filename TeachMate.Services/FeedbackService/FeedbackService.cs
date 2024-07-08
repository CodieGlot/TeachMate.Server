using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Domain;
using TeachMate.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

        public async Task<LearningModuleFeedback?> AddFeedback(LearnerFeedbackDto dto, AppUser appUser)
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

        public async Task<LearningModuleFeedback> LikeFeedback(int feedbackId, AppUser appUser)
        {
            if (appUser?.Id == null)
            {
                throw new InvalidOperationException("AppUser is not valid or does not have a valid Id.");
            }

            var feedback = await _context.LearningModuleFeedbacks.FindAsync(feedbackId);
            if (feedback == null)
            {
                throw new InvalidOperationException($"Feedback with ID {feedbackId} not found.");
            }
            if (await _context.Likes.Where(x => (x.AppUserId == appUser.Id && x.LearningModuleFeedbackId == feedbackId)).AnyAsync())
            {
                var like = await _context.Likes.Where(x => (x.AppUserId == appUser.Id && x.LearningModuleFeedbackId == feedbackId)).FirstAsync();
                _context.Likes.Remove(like);
                feedback.LikeNumber--;

            }
            else
            {
                feedback.LikeNumber++;

                var like = new Like
                {
                    AppUserId = appUser.Id,
                    AppUser = appUser,
                    LearningModuleFeedbackId = feedbackId,
                    LearningModuleFeedback = feedback
                };

                _context.Likes.Add(like);
                
            }
            _context.LearningModuleFeedbacks.Update(feedback);
            await _context.SaveChangesAsync();

            return feedback;
        }

        public async Task<LearningModuleFeedback> DisLikeFeedback(int feedbackId, AppUser appUser)
        {
            if (appUser?.Id == null)
            {
                throw new InvalidOperationException("AppUser is not valid or does not have a valid Id.");
            }

            var feedback = await _context.LearningModuleFeedbacks.FindAsync(feedbackId);
            if (feedback == null)
            {
                throw new InvalidOperationException($"Feedback with ID {feedbackId} not found.");
            }

            if(await _context.Dislikes.Where(x => (x.AppUserId == appUser.Id && x.LearningModuleFeedbackId == feedbackId)).AnyAsync())
            {
                var dislike =await _context.Dislikes.Where(x => (x.AppUserId == appUser.Id && x.LearningModuleFeedbackId == feedbackId)).FirstAsync();
                _context.Dislikes.Remove(dislike);
                feedback.DislikeNumber--;
            }
            else
            {
                feedback.DislikeNumber++;

                var dislike = new Dislike
                {
                    AppUserId = appUser.Id,
                    AppUser = appUser,
                    LearningModuleFeedbackId = feedbackId,
                    LearningModuleFeedback = feedback
                };

                _context.Dislikes.Add(dislike);
            }
            _context.LearningModuleFeedbacks.Update(feedback);
            await _context.SaveChangesAsync();

            return feedback;
        }

        public async Task<List<LearningModuleFeedback>> GetFeedbacksByLearningModuleId(int moduleId)
        {
            return await _context.LearningModuleFeedbacks
                                 .Where(fb => fb.LearningModule.Id == moduleId)
                                 .Include(fb => fb.AppUser)
                                 .Include(fb => fb.TutorReplyFeedback)
                                 .ThenInclude(r => r.Replier)
                                 .ToListAsync();
        }

        public async Task<double> GetAverageRatingByStar(int moduleId)
        {
            var averageRating = await _context.LearningModuleFeedbacks
                                              .Where(fb => fb.LearningModule.Id == moduleId)
                                              .AverageAsync(fb => (double?)fb.Star); 

            return averageRating?? 0.0;
        }


        public async Task<TutorReplyFeedback> ReplyToFeedback(TutorReplyFeedbackDto replyDto, AppUser appUser)
        {
            if (appUser?.Id == null)
            {
                throw new BadRequestException("AppUser is not valid or does not have a valid Id.");
            }

            // Find the original feedback
            var originalFeedback = await _context.LearningModuleFeedbacks.Where(fb => fb.Id == replyDto.LearningModuleFeedbackId).FirstOrDefaultAsync();
            if (originalFeedback == null)
            {
                throw new BadRequestException($"Feedback not found.");
            }

            // Create a new reply
            var tutorReply = new TutorReplyFeedback
            {
                ReplyContent = replyDto.ReplyContent,
                ReplyDate = DateTime.Now,
                ReplierId = appUser.Id,
                LearningModuleFeedbackId = replyDto.LearningModuleFeedbackId,
                LearningModuleFeedback = originalFeedback,
                Replier = appUser
            };

            _context.TutorReplyFeedback.Add(tutorReply);
            await _context.SaveChangesAsync();

            return tutorReply;
        }

         public async Task<TutorReplyFeedback?> GetReplyByFeedbackId(int feedbackId)
        {
            // Query the database to get all replies for the given feedbackId
            var reply = await _context.TutorReplyFeedback
                                        .Where(reply => reply.LearningModuleFeedbackId == feedbackId)
                                        .Include(reply => reply.Replier) // Include the Replier if you need details from AppUser
                                        .FirstOrDefaultAsync();

            return reply;
        }
    }
}

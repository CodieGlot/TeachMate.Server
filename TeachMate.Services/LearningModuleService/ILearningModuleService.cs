using TeachMate.Domain;

namespace TeachMate.Services;
public interface ILearningModuleService
{
    Task<LearningModule> CreateLearningModule(AppUser user, CreateLearningModuleDto dto);
    Task<LearningModule> EnrollLearningModule(Guid learnerId, int moduleId);
    Task<List<LearningModule>> GetAllCreatedModules(AppUser user);
    Task<List<LearningModuleRequest>> GetAllCreatedRequests(Guid requesterId);
    Task<List<LearningModule>> GetAllEnrolledModules(AppUser user);
    Task<List<LearningModuleRequest>> GetAllReceivedRequests(Guid tutorId);
    Task<LearningModule?> GetLearningModuleById(int id);
    Task<LearningModuleRequest?> GetRequestById(int id);
    Task<LearningModuleRequest> UpdateRequestStatus(UpdateRequestStatusDto dto);
    Task<LearningModuleRequest> CreateLearningModuleRequest(AppUser user, CreateLearningModuleRequestDto dto);

    Task<List<LearningModuleRequest>> GetAllReceivedRequests(int moduleId, Guid tutorId);
    Task<List<Learner>> GetAllLearnerInLearningModule(int moduleId, Guid tutorId);
    Task<ResponseDto> OutClass(Guid learnerId, int moduleId);
    Task<ResponseDto> KickLearner(KickLearnerDto dto);

    Task<List<LearningModule>> GetAllLearningModuleOfOneTutor(Guid tutorId);
    Task<double> GetAverageRatingOfTutorByAllLearningModule(Guid tutorId);

    Task<int> GetNumberOfLearnersInAClass(int moduleId);
    Task<ResponseDto> CreateQuestionForSesstion(QuestionDto dto,AppUser appUser);
    Task<ResponseDto> AnswerQuestion(AnswerDto dto,AppUser appUser);
    Task<ResponseDto> Grade(GradeAnswerDto dto);
}
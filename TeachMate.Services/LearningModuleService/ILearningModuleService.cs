using TeachMate.Domain;

namespace TeachMate.Services;
public interface ILearningModuleService
{
    Task<LearningModule> CreateLearningModule(AppUser user, CreateLearningModuleDto dto);
    Task<LearningModule?> GetLearningModuleById(int id);
    Task<List<LearningModule>> GetLearningModulesOfTutor(Guid tutorId);
}
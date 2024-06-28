using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Domain;

namespace TeachMate.Services
{
    public interface ILearningMaterialService
    {
        Task<LearningChapter> AddLearningChapter(AddLearningChapterDto dto);
        Task<LearningMaterial> UploadLearningMaterial(UploadLearningMaterial dto);
        Task<List<LearningMaterial>> GetAllLearningMaterialsByLearningChapterId(int chapterId);
        Task<List<LearningChapter>> GetAllLearningChaptersByLearningModuleId (int moduleId);
    }
}

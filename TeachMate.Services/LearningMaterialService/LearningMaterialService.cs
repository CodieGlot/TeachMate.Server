using Microsoft.EntityFrameworkCore;
using TeachMate.Domain;

namespace TeachMate.Services
{
    public class LearningMaterialService : ILearningMaterialService
    {
        private readonly DataContext _context;

        public LearningMaterialService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<LearningChapter>> GetAllLearningChaptersByLearningModuleId(int moduleId)
        {
            var listChapters = new List<LearningChapter>();
            listChapters = await _context.LearningChapters.Where(x => x.LearningModuleId == moduleId)
                .Include(x => x.LearningMaterials)
                .ToListAsync();
            return listChapters;
        }

        public async Task<List<LearningMaterial>> GetAllLearningMaterialsByLearningChapterId(int chapterId)
        {
            var listMaterials = new List<LearningMaterial>();
            listMaterials = await _context.LearningMaterials.Where(x => x.LearningChapterId == chapterId).ToListAsync();
            return listMaterials;
        }

        public async Task<LearningChapter> AddLearningChapter(AddLearningChapterDto dto)
        {
            var learningChapter = new LearningChapter()
            {
                Description = dto.Description!,
                LearningModuleId = dto.LearningModuleId,
                Name = dto.Name,
            };

            await _context.LearningChapters.AddAsync(learningChapter);
            await _context.SaveChangesAsync();
            return learningChapter;
        }


        public async Task<LearningMaterial> UploadLearningMaterial(UploadLearningMaterialDto dto)
        {
            var learningMaterial = new LearningMaterial()
            {
                DisplayName = dto.DisplayName,
                LearningChapterId = dto.LearningChapterId,
                LinkDownload = dto.LinkDownload,

            };
            await _context.LearningMaterials.AddAsync(learningMaterial);
            await _context.SaveChangesAsync();
            return learningMaterial;
        }
    }
}

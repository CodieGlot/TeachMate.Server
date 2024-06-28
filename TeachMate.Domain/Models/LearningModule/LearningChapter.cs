using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class LearningChapter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = "";

        public List<LearningMaterial> LearningMaterials { get; set; } = new List<LearningMaterial> { };
        public LearningModule LearningModule { get; set; }

        public int LearningModuleId { get; set; }
    }
}

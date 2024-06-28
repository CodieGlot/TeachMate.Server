using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class AddLearningChapterDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int LearningModuleId { get; set; }
    }
}

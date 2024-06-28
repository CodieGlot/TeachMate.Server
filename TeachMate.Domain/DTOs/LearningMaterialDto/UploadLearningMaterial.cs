using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class UploadLearningMaterial
    {
        public string DisplayName { get; set; }
        public string LinkDownload { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public int LearningChapterId { get; set; }

    }
}

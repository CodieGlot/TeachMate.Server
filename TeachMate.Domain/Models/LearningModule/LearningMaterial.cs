using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class LearningMaterial
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string LinkDownload {  get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public int LearningChapterId { get; set; }
        public LearningChapter LearningChapter { get; set; }
        
  
    }
}

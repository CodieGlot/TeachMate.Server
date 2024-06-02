using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class Like
    {
        public int Id { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int LearningModuleFeedbackId { get; set; }
        public LearningModuleFeedback LearningModuleFeedback { get; set; }
    }
}

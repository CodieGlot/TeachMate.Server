using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class TutorReplyFeedback
    {
        public int Id { get; set; }
        public string ReplyContent { get; set; }
        public DateTime ReplyDate { get; set; }
        public Guid ReplierId { get; set; }
        public AppUser Replier { get; set; }
        public int LearningModuleFeedbackId { get; set; }

        // Navigation property to link to LearningModuleFeedback if needed
        public LearningModuleFeedback LearningModuleFeedback { get; set; }
    }
}

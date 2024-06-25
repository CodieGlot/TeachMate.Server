using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class TutorReplyFeedbackDto
    {
        public string ReplyContent { get; set; }  
        public DateTime ReplyDate { get; set; } = DateTime.Now;  // Date and time of the reply

        public int LearningModuleFeedbackId { get; set; }

    }
}

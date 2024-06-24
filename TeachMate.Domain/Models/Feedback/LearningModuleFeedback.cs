using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class LearningModuleFeedback
    {
        [Key]
        public int Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        [Range(1, 5)]
        public int Star { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public LearningModule LearningModule { get; set; } 
        public AppUser AppUser { get; set; }
        public int LikeNumber { get; set; } = 0;
        public int DislikeNumber { get; set; } = 0;
        public bool IsAnonymous { get; set; } = false;

        public TutorReplyFeedback TutorReplyFeedback { get; set; }
    }

}

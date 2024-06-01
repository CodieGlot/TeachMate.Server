using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;


namespace TeachMate.Domain
{
    public class LearnerFeedbackDto
    {
        public string Comment { get; set; } = string.Empty;
        [Range(1, 5)]
        public int Star { get; set; }
        public int LearningModuleId { get; set; }
     /*   public int LikeNumber { get; set; } = 0;
        public int DislikeNumber { get; set; } = 0;*/
        public bool IsAnonymous { get; set; } = false;
    

    }
}

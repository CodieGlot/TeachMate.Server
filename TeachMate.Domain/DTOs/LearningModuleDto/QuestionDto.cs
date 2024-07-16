using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class QuestionDto
    {
        public string? context {  get; set; }
        public int LearningSesstionID { get; set; }
        public int? AnswerId { get; set; }

    }
}

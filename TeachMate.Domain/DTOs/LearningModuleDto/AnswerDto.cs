using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class AnswerDto
    {
        public string? Context { get; set; }
        public int QuestionId { get; set; }
        public string? TutorComment { get; set; }
        public int? grade { get; set; } = null;
    }
}

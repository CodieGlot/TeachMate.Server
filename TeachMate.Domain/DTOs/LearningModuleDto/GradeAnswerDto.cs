using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class GradeAnswerDto
    {
        public int Grade { get; set; }
        public int answerID { get; set; }
        public string Comments { get; set; }
    }
}

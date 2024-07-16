using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class Question
    {
        public int Id { get; set; }
        public string? Context { get; set; }

        public int LearningSessionId { get; set; }
        public  int? AnswerId { get; set; }
        public Guid TutorID { get; set; }
        public virtual LearningSession LearningSession { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

     }
}

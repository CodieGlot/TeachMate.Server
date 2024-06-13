using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain.DTOs.ScheduleDto
{
    public class CreateCustomLearningDto
    { 

        public string Title { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public string LinkMeet { get; set; }

        public int LearningModuleId { get; set; }
    }
}

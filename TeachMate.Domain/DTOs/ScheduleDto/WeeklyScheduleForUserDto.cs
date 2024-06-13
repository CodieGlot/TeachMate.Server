using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain.DTOs.ScheduleDto
{
    public class WeeklyScheduleForUserDto
    {
        List<LearningSession> LearningSessions { get; set; }
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }

    }
}

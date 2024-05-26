using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class WeeklySlot
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int WeeklyScheduleId { get; set; }
        public WeeklySchedule WeeklySchedule { get; set; }
    }
}

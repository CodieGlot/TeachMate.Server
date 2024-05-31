using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain.DTOs.ScheduleDto
{
    public class AddWeeklyScheduleDto
    {
        public List<WeeklySlotDto> WeeklySlots { get; set; }
        public int LearningModuleId { get; set; }
    }
}

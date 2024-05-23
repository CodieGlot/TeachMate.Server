using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain.Models.Schedule
{
    public class WeeklySchedule
    {
        public int Id { get; set; }
        public int NumberOfSlot { get; set; }

        List<WeeklySlot> WeeklySlots { get; set; } = new List<WeeklySlot>();
    }
}

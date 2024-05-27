namespace TeachMate.Domain
{
    public class WeeklySchedule
    {
        public int Id { get; set; }
        public int NumberOfSlot { get; set; }

        public List<WeeklySlot> WeeklySlots { get; set; } = new List<WeeklySlot>();
    }
}

namespace TeachMate.Domain;
public class CreateLearningSessionDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Subject Subject { get; set; } = Subject.None;
    // Calculated in minutes
    public int Duration { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int MaximumLearners { get; set; }
}

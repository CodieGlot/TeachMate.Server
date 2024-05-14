namespace TeachMate.Services;
public class LearningSessionService
{
    private readonly DataContext _context;

    public LearningSessionService(DataContext context)
    {
        _context = context;
    }
}

namespace TeachMate.Domain;

public class ReportUserDto
{
    public TypeErrorUser typeErrorUser { get; set; }
    public String Title { get; set; } = String.Empty;
    public String Description { get; set; } = String.Empty;
}

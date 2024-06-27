using System.ComponentModel.DataAnnotations;

namespace TeachMate.Domain;

public class ReportUser
{
    [Key]
    public int Id { get; set; }
    public TypeErrorUser typeErrorUser { get; set; }
    public Guid UserIdReported { get; set; }
}

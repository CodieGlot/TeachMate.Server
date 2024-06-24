using System.ComponentModel.DataAnnotations;

namespace TeachMate.Domain;

public class ReportSystem 
{
    [Key]
    public int Id { get; set; }
    public TypeErrorSystem typeErrorSystem {  get; set; }   
}

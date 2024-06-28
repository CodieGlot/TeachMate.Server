using System.ComponentModel.DataAnnotations;

namespace TeachMate.Domain;

public class SystemReport 
{
    [Key]
    public int Id { get; set; }
    public SystemReportType SystemReportType {  get; set; }   
}

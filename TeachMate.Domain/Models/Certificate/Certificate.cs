using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain.Models.Certificate
{
    public class Certificate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateClaimed { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CertificateFile { get; set; } 
        public Guid TutorId { get; set; }
        public Tutor Tutor { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class UploadCertificateDto
    {
        public string Title { get; set; }
        public DateTime DateClaimed { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CertificateFile { get; set; }
    }
}

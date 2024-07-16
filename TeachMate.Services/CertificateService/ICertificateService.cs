using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Domain;
using TeachMate.Domain.Models.Certificate;

namespace TeachMate.Services
{
    public interface ICertificateService
    {
        Task<Certificate> UploadCertificate(UploadCertificateDto dto, Guid tutorId);
        Task<List<Certificate>> GetAllCertificatesByTutorId(Guid tutorId);
    }
}

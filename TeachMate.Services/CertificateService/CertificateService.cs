using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Domain;
using TeachMate.Domain.Models.Certificate;

namespace TeachMate.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly DataContext _context;
        public CertificateService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Certificate>> GetAllCertificatesByTutorId(Guid tutorId)
        {
            var certificates = new List<Certificate>();
            certificates = await _context.Certificates.Where(c => c.TutorId == tutorId).ToListAsync();
            return certificates;
        }
      
        public async Task<Certificate> UploadCertificate(UploadCertificateDto dto, Guid tutorId)
        {
            var c = new Certificate()
            {
                CertificateFile = dto.CertificateFile,
                DateClaimed = dto.DateClaimed,
                Description = dto.Description,
                Title = dto.Title,
                TutorId = tutorId,
            };
            await _context.Certificates.AddAsync(c);
            await _context.SaveChangesAsync();
            return c;
        }
    }
}

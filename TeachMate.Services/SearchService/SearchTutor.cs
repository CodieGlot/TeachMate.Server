using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Domain;

namespace TeachMate.Services.SearchService
{
    public class SearchTutor : ISearchTotor
    {
        private readonly DataContext _context;
        public SearchTutor(DataContext context)
        {
            _context = context;
        }

        public async Task<AppUser> SearchTutorById(Guid id)
        {
            // Retrieve the Tutor by ID
            var tutor = await _context.Tutors.Include(t => t.AppUser).FirstOrDefaultAsync(t => t.Id == id);

            // Check if the tutor is found
            if (tutor == null)
            {
                throw new KeyNotFoundException("Tutor not found");
            }

            // Return the associated AppUser
            return tutor.AppUser;
        }
    }
}

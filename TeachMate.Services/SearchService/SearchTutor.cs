﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Domain;
using TeachMate.Services;

namespace TeachMate.Services
{
    public class SearchTutor : ISearchTutor
    {
        private readonly DataContext _context;
        public SearchTutor(DataContext context)
        {
            _context = context;
        }

        public async Task<List<AppUser>> Search(string? DisplayName)
        {
            if(DisplayName.IsNullOrEmpty())
            {
                var tutorAll = await _context.Tutors
                                       .Include(t => t.AppUser)
                                       .Select(t => t.AppUser)
                                       .ToListAsync();
                if (tutorAll == null)
                {
                    throw new KeyNotFoundException("Tutor not found");
                }
                return tutorAll;
            }
            // Retrieve the Tutor by ID
            var tutor = await _context.Tutors
                                       .Include(t => t.AppUser)
                                       .Where(t => t.AppUser.DisplayName.ToLower().Contains(DisplayName.ToLower()))
                                       .Select(t => t.AppUser)
                                       .ToListAsync();

            // Check if the tutor is found
            if (tutor == null)
            {
                throw new KeyNotFoundException("Tutor not found");
            }

            // Return the associated AppUser
            return tutor;
        }
    }
}

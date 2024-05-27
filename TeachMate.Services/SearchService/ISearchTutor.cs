﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Domain;
using TeachMate.Domain.DTOs.SearchDto;

namespace TeachMate.Service.SearchService;
public interface ISearchTutor
{
    Task<List<AppUser>> Search(string DisplayName);
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Domain;
using TeachMate.Domain.DTOs.SearchDto;

namespace TeachMate.Services.SearchService
{
    public interface ISearchClass
    {
        Task<List<LearningModule>> Search(SearchClassDto dto); //truyen vo data user enter
    }
}

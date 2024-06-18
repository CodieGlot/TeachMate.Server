﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class UpdateLearnerDetailDto
    {


        public string DisplayName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string PhoneNumber { get; set; } = "";
        public string Avatar { get; set; } = "";
        public int GradeLevel { get; set; }
    }
}

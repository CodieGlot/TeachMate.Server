﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class Answer
    {
        public int Id { get; set; }
        public Guid LearnerId { get; set; }
        public string? Context { get; set; }
        public string QuestionId { get; set; }
        public string? TutorComment { get; set; }
        public int? Grade { get; set; } 
        public Question Question { get; set; }
    }
}

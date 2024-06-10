using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class SearchClassDto
    {
        public string TitleOrDesc { get; set; }
        public Subject? Subject { get; set; } // Nullable enum
        public int? GradeLevel { get; set; }
        public DateOnly? StartOpenDate { get; set; }
        public DateOnly? EndOpenDate { get; set; }
        public int? MaximumLearners { get; set; }
        public ModuleType? ModuleType { get; set; }
        public int? NumOfWeeks { get; set; }
    }
}

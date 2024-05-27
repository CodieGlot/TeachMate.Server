using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TeachMate.Domain.Models.Schedule;

namespace TeachMate.Domain.DTOs.SearchDto
{
    public class SearchTutorDto
    {
        public string DisplayName { get; set; } = string.Empty;
    }
}

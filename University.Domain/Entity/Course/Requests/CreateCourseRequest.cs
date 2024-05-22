using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Domain.Entity.Course.Requests
{
    public class CreateCourseRequest
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; } = null!;
    }
}

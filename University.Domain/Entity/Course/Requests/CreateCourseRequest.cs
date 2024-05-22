using System.ComponentModel.DataAnnotations;

namespace University.Domain.Entity.Course.Requests
{
    public class CreateCourseRequest
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; } = null!;
    }
}

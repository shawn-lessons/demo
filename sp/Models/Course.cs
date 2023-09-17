using System.ComponentModel.DataAnnotations;
namespace sp.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string CourseId { get; set; }

        [Required]
        public string Title { get; set; }

    }
}
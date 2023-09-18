using System.ComponentModel.DataAnnotations;
namespace sp.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
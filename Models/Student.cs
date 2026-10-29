
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter student name")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Please enter roll number")]
        public string RollNumber { get; set; } = "";

        [Required(ErrorMessage = "Please enter course")]
        public string Course { get; set; } = "";

        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; } = "";
    }
}

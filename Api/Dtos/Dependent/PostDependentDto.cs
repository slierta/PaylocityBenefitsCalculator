using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.Dtos.Dependent
{
    public class PostDependentDto
    {
        [Required(AllowEmptyStrings = false)]
        public string? FirstName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string? LastName { get; set; }
        [Required] 
        public DateTime DateOfBirth { get; set; }
        [Required] 
        public Relationship Relationship { get; set; }
    }
}
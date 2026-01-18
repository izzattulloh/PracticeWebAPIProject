using System.ComponentModel.DataAnnotations;

namespace PracticeWebAPIProject.Models.DTOs;

public class CreateUserDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
}
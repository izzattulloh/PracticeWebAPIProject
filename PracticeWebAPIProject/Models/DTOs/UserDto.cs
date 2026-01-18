using System.ComponentModel.DataAnnotations;

namespace PracticeWebAPIProject.Models.DTOs;

public class UserDto : BaseEntity
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
}
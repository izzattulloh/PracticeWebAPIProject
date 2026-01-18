using System.ComponentModel.DataAnnotations;

namespace PracticeWebAPIProject.Models;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}
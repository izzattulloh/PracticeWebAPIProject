namespace PracticeWebAPIProject.Models;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; } = "example@gmail.com";
    
    public List<Post> Posts { get; set; } = new();
}
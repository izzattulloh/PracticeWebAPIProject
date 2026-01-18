namespace PracticeWebAPIProject.Models;

public class Post : BaseEntity
{
    public string Title { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
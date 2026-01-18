namespace PracticeWebAPIProject.Models.DTOs;

public class UserWithPostsDto : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public IEnumerable<PostDto> Posts { get; set; }
}
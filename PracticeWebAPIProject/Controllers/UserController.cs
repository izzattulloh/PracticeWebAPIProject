using Microsoft.AspNetCore.Mvc;
using PracticeWebAPIProject.Models;
using PracticeWebAPIProject.Models.DTOs;
using PracticeWebAPIProject.Services;

namespace PracticeWebAPIProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        if (result is null)
        {
            return NotFound($"User with ID: {id} doesn't exist");
        }
        return Ok(new UserDto(){Id = result.Id, Name =  result.Name});
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create(CreateUserDto dto)
    {
        var user = new User()
        {
            Name = dto.Name,
            Email = dto.Email,
        };
        
        var createdUser = await service.CreateAsync(user);

        var result = new UserDto()
        {
            Id = createdUser.Id,
            Name = createdUser.Name
        };
        
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll()
    {
        var users = await service.GetAllAsync();

        var result = users.Select(u => new UserDto()
        {
            Id = u.Id,
            Name = u.Name
        }).ToList();
        
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, UpdateUserDto dto)
    {
        var updated = await service.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound($"User Not Found with ID: {id}");
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await service.DeleteAsync(id);

        if (!deleted)
            return NotFound();
        
        return NoContent();
    }
    
    [HttpGet("{id:int}/withposts")]
    public async Task<ActionResult<UserWithPostsDto>> GetUser(int id)
    {
        var user = await service.GetUserWithPostsAsync(id);

        if (user is null)
            return NotFound();

        var result = new UserWithPostsDto
        {
            Id = user.Id,
            Name = user.Name,
            Posts = user.Posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title
            }).ToList()
        };

        return Ok(result);
    }
}
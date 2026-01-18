using Microsoft.EntityFrameworkCore;
using PracticeWebAPIProject.Data;
using PracticeWebAPIProject.Models;
using PracticeWebAPIProject.Models.DTOs;

namespace PracticeWebAPIProject.Services;

public class UserService(UserDbContext context) : IUserService
{
    public async Task<User> CreateAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = await context.Users.FindAsync(id);

        if (user is null)
            return false;

        user.Name = dto.Name is not null ? dto.Name : user.Name;
        user.Email = dto.Email is not null ? dto.Email : user.Email;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await context.Users.FindAsync(id);

        if (user is null)
            return false;
        
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<User?> GetUserWithPostsAsync(int id)
    {
        return await context.Users
            .Include(u => u.Posts)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}

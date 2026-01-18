using PracticeWebAPIProject.Models;
using PracticeWebAPIProject.Models.DTOs;

namespace PracticeWebAPIProject.Services;

public interface IUserService
{
    public Task<User> CreateAsync(User user);
    public Task<User?> GetByIdAsync(int id);
    public Task<List<User>> GetAllAsync();
    public Task<bool> UpdateAsync(int id, UpdateUserDto dto);
    public Task<bool> DeleteAsync(int id);
    public Task<User?> GetUserWithPostsAsync(int id);
}
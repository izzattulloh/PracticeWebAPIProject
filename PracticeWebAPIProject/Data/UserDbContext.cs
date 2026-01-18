using Microsoft.EntityFrameworkCore;
using PracticeWebAPIProject.Models;

namespace PracticeWebAPIProject.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
}
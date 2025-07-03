using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Models;

namespace ToDoListAPI.Context;

internal class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<ToDo> ToDos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            user.Property(prop => prop.IsActive).HasDefaultValue(true);
            user.Property(prop => prop.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
        modelBuilder.Entity<ToDo>().Property(prop => prop.Status).HasDefaultValue(Status.Pending);
    }
}
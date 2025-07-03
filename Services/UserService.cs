using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Context;
using ToDoListAPI.DTOs;
using ToDoListAPI.Interfaces;
using ToDoListAPI.Models;
using ToDoListAPI.Utils;

namespace ToDoListAPI.Services;

internal class UserService(AppDbContext dbContext) : IUserService
{
    private readonly AppDbContext _context = dbContext;

    public async Task<int> CreateUser(RegisterUser user)
    {
        User? userValidate = await _context.Users.FirstOrDefaultAsync(alreadyExistsUser => alreadyExistsUser.Email == user.Email);

        if (userValidate != null) return 0;
        if (!Auth.ValidateEmail(user.Email)) return 0;

        string hashedPassword = Auth.GeneratePassword(user.Password);
        User newUser = new(user.Name, user.Email, hashedPassword);

        try
        {
            await _context.Users.AddAsync(newUser);
            int rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during user creation: {ex.Message}");
            return 0;
        }
    }

    public async Task<int> DeleteUser(int userId)
    {
        User? currentUser = await _context.Users.FindAsync(userId);

        try
        {
            if (currentUser != null)
            {
                currentUser.IsActive = false;

                int result = await _context.SaveChangesAsync();

                return result;
            }

            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during user deletion: {ex.Message}");
            return 0;
        }
    }

    public async Task<User?> GetUser(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<User?> GetUser(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<List<User>?> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<int> UpdateUser(int userId, UpdateUser user)
    {
        User? currentUser = await _context.Users.FindAsync(userId);

        try
        {
            if (currentUser != null)
            {
                currentUser.Name = user?.Name != null ? user.Name : currentUser.Name;
                currentUser.Password = user?.Password != null && user?.Password.Trim().Length > 0 ? Auth.GeneratePassword(user.Password) : currentUser.Password;

                int rowsAffected = await _context.SaveChangesAsync();

                return rowsAffected;
            }

            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during user update: {ex.Message}");
            return 0;
        }
    }
}
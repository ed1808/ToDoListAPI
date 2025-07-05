using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Context;
using ToDoListAPI.DTOs;
using ToDoListAPI.Exceptions;
using ToDoListAPI.Interfaces;
using ToDoListAPI.Models;
using ToDoListAPI.Utils;

namespace ToDoListAPI.Services;

internal class UserService(AppDbContext dbContext) : IUserService
{
    private readonly AppDbContext _context = dbContext;
    private const string _notFoundMessage = "User not found";

    public async Task<int> CreateUser(RegisterUser user)
    {
        User? userValidate = await _context.Users.FirstOrDefaultAsync(alreadyExistsUser => alreadyExistsUser.Email == user.Email);

        if (userValidate != null) throw new ConflictException("User already exists");

        ValidateUserData(user);

        string hashedPassword = Auth.GeneratePassword(user.Password);
        User newUser = new(user.Name, user.Email, hashedPassword);

        await _context.Users.AddAsync(newUser);
        int rowsAffected = await _context.SaveChangesAsync();

        return rowsAffected;
    }

    public async Task<int> DeleteUser(int userId)
    {
        User? currentUser = await _context.Users.FindAsync(userId);

        if (currentUser != null)
        {
            currentUser.IsActive = false;

            int result = await _context.SaveChangesAsync();

            return result;
        }

        throw new NotFoundException(_notFoundMessage);
    }

    public async Task<User> GetUser(string email)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

        if (user != null) return user;

        throw new NotFoundException(_notFoundMessage);
    }

    public async Task<User> GetUser(int id)
    {
        User? user = await _context.Users.FindAsync(id);

        if (user != null) return user;

        throw new NotFoundException(_notFoundMessage);
    }

    public async Task<List<User>> GetUsers()
    {
        List<User>? users = await _context.Users.ToListAsync();
        return users ?? [];
    }

    public async Task<int> UpdateUser(int userId, UpdateUser user)
    {
        User? currentUser = await _context.Users.FindAsync(userId);

        if (currentUser != null)
        {
            currentUser.Name = user?.Name != null ? user.Name : currentUser.Name;
            currentUser.Password = user?.Password != null && user?.Password.Trim().Length > 0 ? Auth.GeneratePassword(user.Password) : currentUser.Password;

            int rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected;
        }

        throw new NotFoundException(_notFoundMessage);
    }

    /// <summary>
    /// Valida los datos del usuario antes de crear el registro.
    /// </summary>
    /// <param name="user">Datos del usuario a validar.</param>
    /// <exception cref="ValidationException">Si algún dato requerido es inválido.</exception>
    /// <exception cref="InvalidEmailException">Si el correo es inválido.</exception>
    private static void ValidateUserData(RegisterUser user)
    {
        if (string.IsNullOrEmpty(user.Email.Trim())) throw new ValidationException("Email required");
        if (!Auth.ValidateEmail(user.Email)) throw new InvalidEmailException();

        if (string.IsNullOrEmpty(user.Name.Trim())) throw new ValidationException("Name required");

        if (string.IsNullOrEmpty(user.Password.Trim())) throw new ValidationException("Password required");
    }
}
using ToDoListAPI.DTOs;
using ToDoListAPI.Models;

namespace ToDoListAPI.Interfaces;

internal interface IUserService
{
    Task<int> CreateUser(RegisterUser user);
    Task<User?> GetUser(string email);
    Task<User?> GetUser(int id);
    Task<List<User>?> GetUsers();
    Task<int> UpdateUser(int userId, UpdateUser user);
    Task<int> DeleteUser(int userId);
}
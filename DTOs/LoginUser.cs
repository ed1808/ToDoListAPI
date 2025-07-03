namespace ToDoListAPI.DTOs;

internal class LoginUser(string email, string password)
{
    string Email { get; set; } = email;
    string Password { get; set; } = password;
}
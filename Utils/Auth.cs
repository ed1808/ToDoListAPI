using System.Text.RegularExpressions;
using BCrypt.Net;

namespace ToDoListAPI.Utils;

internal static class Auth
{
    public static string GeneratePassword(string password)
    {
        return BC.EnhancedHashPassword(password, HashType.SHA512);
    }

    public static bool VerifyPassword(string password, string storedPassword)
    {
        return BC.EnhancedVerify(password, storedPassword, HashType.SHA512);
    }

    public static bool ValidateEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        Match result = Regex.Match(email, pattern);

        return result.Success;
    }
}
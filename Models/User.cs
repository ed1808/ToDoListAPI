using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ToDoListAPI.Models;

[Table("Users")]
[Index(nameof(Email), IsUnique = true)]
internal class User(string name, string email, string password, bool? isActive = null)
{
    [Key]
    public int Id { get; set; }

    [MaxLength(150)]
    public string Name { get; set; } = name;

    [MaxLength(200)]
    public string Email { get; set; } = email;

    public string Password { get; set; } = password;

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; } = isActive;

    public ICollection<ToDo> ToDos { get; } = [];
}
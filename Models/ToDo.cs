using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListAPI.Models;

[Table("Todos")]
internal class ToDo(string title, string description, Priority priority, Status? status = null, bool? remind = null, DateTime? reminderDate = null)
{
    [Key]
    public int Id { get; set; }

    [MaxLength(150)]
    public string Title { get; set; } = title;

    public string Description { get; set; } = description;

    public Priority Priority { get; set; } = priority;

    public Status? Status { get; set; } = status;

    public bool? Remind { get; set; } = remind;

    public DateTime? ReminderDate { get; set; } = reminderDate;

    public bool? IsNotified { get; set; } = null;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }

    public User User { get; set; } = null!;
}

internal enum Priority
{
    Low,
    Medium,
    High,
    Urgent,
}

internal enum Status
{
    Pending,
    InProcess,
    Finished
}
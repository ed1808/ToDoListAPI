using ToDoListAPI.Models;

namespace ToDoListAPI.DTOs;

internal class CreateToDo(string title, string description, Priority priority, bool? remind, DateTime? reminderDate, int userId)
{
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
    public Priority Priority { get; set; } = priority;
    public bool? Remind { get; set; } = remind;
    public DateTime? ReminderDate { get; set; } = reminderDate;
    public int UserId { get; set; } = userId;
}
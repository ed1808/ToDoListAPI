namespace ToDoListAPI.Exceptions;

public class ConflictException(string? message) : Exception(message) {  }
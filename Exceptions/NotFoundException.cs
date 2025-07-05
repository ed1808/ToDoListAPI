namespace ToDoListAPI.Exceptions;

public class NotFoundException(string? message) : Exception(message) {  }
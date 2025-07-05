namespace ToDoListAPI.Exceptions;

public class InvalidEmailException(string? message = "Invalid email") : Exception (message) {  }
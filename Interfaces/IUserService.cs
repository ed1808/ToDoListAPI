using ToDoListAPI.DTOs;
using ToDoListAPI.Models;

namespace ToDoListAPI.Interfaces;

internal interface IUserService
{
    /// <summary>
    /// Crea un nuevo usuario en la base de datos.
    /// </summary>
    /// <param name="user">Datos del usuario a registrar.</param>
    /// <returns>Número de filas afectadas.</returns>
    /// <exception cref="ConflictException">Si el usuario ya existe.</exception>
    /// <exception cref="InvalidEmailException">Si el correo es inválido.</exception>
    /// <exception cref="ValidationException">Si algún dato requerido es inválido.</exception>
    Task<int> CreateUser(RegisterUser user);

    /// <summary>
    /// Obtiene un usuario por su correo electrónico.
    /// </summary>
    /// <param name="email">Correo electrónico del usuario.</param>
    /// <returns>El usuario encontrado.</returns>
    /// <exception cref="NotFoundException">Si el usuario no existe.</exception>
    Task<User> GetUser(string email);

    /// <summary>
    /// Obtiene un usuario por su ID.
    /// </summary>
    /// <param name="id">ID del usuario.</param>
    /// <returns>El usuario encontrado.</returns>
    /// <exception cref="NotFoundException">Si el usuario no existe.</exception>
    Task<User> GetUser(int id);

    /// <summary>
    /// Obtiene la lista de todos los usuarios.
    /// </summary>
    /// <returns>Lista de usuarios.</returns>
    Task<List<User>> GetUsers();

    /// <summary>
    /// Actualiza los datos de un usuario existente.
    /// </summary>
    /// <param name="userId">ID del usuario a actualizar.</param>
    /// <param name="user">Datos nuevos del usuario.</param>
    /// <returns>Número de filas afectadas.</returns>
    /// <exception cref="NotFoundException">Si el usuario no existe.</exception>
    Task<int> UpdateUser(int userId, UpdateUser user);

    /// <summary>
    /// Elimina (desactiva) un usuario por su ID.
    /// </summary>
    /// <param name="userId">ID del usuario a eliminar.</param>
    /// <returns>Número de filas afectadas.</returns>
    /// <exception cref="NotFoundException">Si el usuario no existe.</exception>
    Task<int> DeleteUser(int userId);
}
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.DTOs;
using ToDoListAPI.Exceptions;
using ToDoListAPI.Interfaces;

namespace ToDoListAPI.Endpoints;

internal static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/register", RegisterUserHandler);
        app.MapGet("api/users", GetUsersHandler);
        app.MapGet("api/users/{id}", GetUserByIdHandler);
        app.MapPut("api/users/{id}", UpdateUserHandler);
        app.MapDelete("api/users/{id}", DeleteUserHandler);
    }

    private static async Task<IResult> RegisterUserHandler([FromServices] IUserService userService, [FromBody] RegisterUser registerUser)
    {
        try
        {
            int result = await userService.CreateUser(registerUser);

            return result == 0
                ? Results.BadRequest(new { Error = "Something went wrong" })
                : Results.Created();

        }
        catch (ValidationException ex)
        {
            return Results.BadRequest(new { Error = ex.Message });
        }
        catch (InvalidEmailException ex)
        {
            return Results.BadRequest(new { Error = ex.Message });
        }
        catch (ConflictException ex)
        {
            return Results.BadRequest(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { Error = ex.Message });
        }
    }

    private static async Task<IResult> GetUsersHandler([FromServices] IUserService userService)
    {
        return Results.Ok(await userService.GetUsers());
    }

    private static async Task<IResult> GetUserByIdHandler([FromServices] IUserService userService, [FromRoute] string id)
    {
        bool parseResult = int.TryParse(id, out int userId);

        if (!parseResult) return Results.BadRequest(new { Error = "Invalid ID type" });

        try
        {
            return Results.Ok(await userService.GetUser(userId));
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(new { Error = ex.Message });
        }
        
    }

    private static async Task<IResult> UpdateUserHandler([FromServices] IUserService userService, [FromRoute] string id, [FromBody] UpdateUser updateUser)
    {
        bool parseResult = int.TryParse(id, out int userId);

        if (!parseResult) return Results.BadRequest(new { Error = "Invalid ID type" });

        try
        {
            int result = await userService.UpdateUser(userId, updateUser);

            return result == 0
                ? Results.BadRequest(new { Error = "Something went wrong" })
                : Results.NoContent();
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }

    private static async Task<IResult> DeleteUserHandler([FromServices] IUserService userService, [FromRoute] string id)
    {
        bool parseResult = int.TryParse(id, out int userId);

        if (!parseResult) return Results.BadRequest(new { Error = "Invalid ID type" });

        try
        {
            int result = await userService.DeleteUser(userId);

            return result == 0
                ? Results.BadRequest(new { Error = "Something went wrong" })
                : Results.NoContent();
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
}
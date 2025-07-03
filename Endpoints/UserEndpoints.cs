using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.DTOs;
using ToDoListAPI.Interfaces;

namespace ToDoListAPI.Endpoints;

internal static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/register", async ([FromServices] IUserService userService, [FromBody] RegisterUser registerUser) =>
        {
            int result = await userService.CreateUser(registerUser);

            return result == 0
                ? Results.BadRequest(new { Error = "Something went wrong" })
                : Results.Created();
        });

        app.MapGet("api/users", async ([FromServices] IUserService userService) =>
        {
            return Results.Ok(await userService.GetUsers());
        });

        app.MapGet("api/users/{id}", async ([FromServices] IUserService userService, [FromRoute] string id) =>
        {
            bool parseResult = int.TryParse(id, out int userId);

            if (!parseResult) return Results.BadRequest(new { Error = "Invalid ID type" });

            return Results.Ok(await userService.GetUser(userId));
        });

        app.MapPut("api/users/{id}", async ([FromServices] IUserService userService, [FromRoute] string id, [FromBody] UpdateUser updateUser) =>
        {
            bool parseResult = int.TryParse(id, out int userId);

            if (!parseResult) return Results.BadRequest(new { Error = "Invalid ID type" });

            int result = await userService.UpdateUser(userId, updateUser);

            return result == 0
                ? Results.BadRequest(new { Error = "Something went wrong" })
                : Results.NoContent();
        });

        app.MapDelete("api/users/{id}", async ([FromServices] IUserService userService, [FromRoute] string id) =>
        {
            bool parseResult = int.TryParse(id, out int userId);

            if (!parseResult) return Results.BadRequest(new { Error = "Invalid ID type" });

            int result = await userService.DeleteUser(userId);

            return result == 0
                ? Results.BadRequest(new { Error = "Something went wrong" })
                : Results.NoContent();
        });
    }
}
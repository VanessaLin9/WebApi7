namespace WebApplication7.Endpoints;

public static class Todos
{
    public static IEndpointRouteBuilder MapTodosApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Todos");
        group.MapGet("/", GetTodos);
        return app;
    }

    private static IResult GetTodos(HttpContext context)
    {
        return Results.Ok("okay");
    }
}
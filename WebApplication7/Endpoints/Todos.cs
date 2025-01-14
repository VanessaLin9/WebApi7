using System.Text;
using Newtonsoft.Json;


namespace WebApplication7.Endpoints;

public static class Todos
{
    public static IEndpointRouteBuilder MapTodosApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Todos");
        group.MapGet("/", GetTodos);
        group.MapPost("/login", LoginToA);
        return app;
    }

    private static async Task<IResult> LoginToA(HttpClient client, HttpContext context)
    {
        var request = new Request
        {
            UserName = "vv",
            Id = "12345"
        };
        var httpRequestMessage = new HttpRequestMessage();
        httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("http://localhost:5222/Lunch/getaa", httpRequestMessage.Content);
        return Results.Ok(JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync()));
    }

    private static IResult GetTodos(HttpContext context)
    {
        return Results.Ok("okay");
    }
}

internal class Request
{
    public required string UserName { get; set; }
    public required string Id { get; set; }
}
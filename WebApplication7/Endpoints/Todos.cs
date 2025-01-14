using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc.Authorization;
using Newtonsoft.Json;
using WebApplication7.Filter;


namespace WebApplication7.Endpoints;

public static class Todos
{
    public static IEndpointRouteBuilder MapTodosApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Todos");
        group.MapGet("/", GetTodos);
        group.MapPost("/login", LoginToA).AddEndpointFilter<AuthorizationFilter>();
        return app;
    }

    private static async Task<IResult> LoginToA(HttpClient client, UserRequest request)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(new Request
        {
            UserName = request.Name,
            Id = request.Id.ToString()
        }), Encoding.UTF8, "application/json");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "JwtHeader.JwtPayload.Signature");
        var response = await client.PostAsync("http://localhost:5222/Lunch/getaa", stringContent);
        return Results.Ok(JsonConvert.DeserializeObject<LoginResponse>(await response.Content.ReadAsStringAsync()));
    }

    private static IResult GetTodos(HttpContext context)
    {
        return Results.Ok("okay");
    }
}

internal class LoginResponse
{
    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("time")]
    public DateTime Time { get; set; }
}

internal class UserRequest
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }
}

internal class Request
{
    public required string UserName { get; set; }
    public required string Id { get; set; }
}
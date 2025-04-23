var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddOpenApi();

var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.MapGet("/", () => "Welcome to the Coffee Shop API! Visit /swagger to explore our brew-tiful API.");

    app.MapPost("/signup", (User user) =>
    {
        if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
        {
            return Results.BadRequest("Name and Email are required.");
        }

        return Results.Ok("Sign-up successful!");
    });

    app.Run();

public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
}

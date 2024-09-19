namespace Reader.API.EndPoints;

public static class HealthCheckEndpoints
{
    public static void MapHealthCheckEndpoints(this WebApplication app)
    {
        app.MapGet("api/v1/ping", () => "Pong!");
    }
}

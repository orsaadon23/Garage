using Microsoft.AspNetCore.Builder;

public static class GarageApi
{
    public static void RegisterGarageEndpoints(this WebApplication app)
    {
        app.MapGet("/api/vehicles", () => "Get all vehicles endpoint");
    }
}

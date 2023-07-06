namespace SiteNinja.Middleware
{
    public record ErrorResponse(string message, string? userMessage, string? errorDetails);
}

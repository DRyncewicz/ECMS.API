namespace ecms.API.OpenApi;

public class SwaggerOptions
{
    public string AuthorizationUrl { get; set; }
    public string TokenUrl { get; set; }
    public Dictionary<string, string> Scopes { get; set; }

    public const string SectionName = "Swagger";
}
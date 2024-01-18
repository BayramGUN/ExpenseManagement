namespace ExpenseManagement.Base.Swagger;

public abstract class SwaggerConfigurationKeys : SwaggerSecuritySchemeKeys
{
    public const string VersionId = "v1";
    public const string Version = "v1.0";
    public const string Title = "Expense Management Api";
}

public abstract class SwaggerSecuritySchemeKeys
{
    public const string Name = "Expense Management for Companies";
    public const string Description = "Enter JWT Bearer token **_only_**";
    public const string Scheme = "bearer";
    public const string Format = "JWT";

}
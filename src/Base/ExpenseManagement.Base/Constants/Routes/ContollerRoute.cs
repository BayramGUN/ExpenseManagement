namespace ExpenseManagement.Base.Routes;

public class ControllerRoute
{
    public const string BaseRoute = "api/[controller]";
}

public class EndpointRoute
{
    public const string Hello = "/hello";
    public const string SignIn = "SingIn";
    public const string SignUp = "SingUp";
    public const string UnBlockAppUser = "UnBlockAppUser";
    public const string GetAppUserById = "GetAppUserById/{id}";
    public const string GetAppUserBy = "GetAppUserBy";
    public const string Delete = "Delete/{id}";
    public const string Update = "Update";
    public const string Create = "Create";
    

}
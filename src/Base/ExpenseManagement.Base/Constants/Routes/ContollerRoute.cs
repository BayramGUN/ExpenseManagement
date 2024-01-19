namespace ExpenseManagement.Base.Routes;

public class ControllerRoute
{
    public const string BaseRoute = "api/[controller]s";
}

public class EndpointRoute
{
    public const string Hello = "/hello";
    public const string SignIn = "SignIn";
    public const string SignUp = "SignUp";
    public const string UnBlockAppUser = "UnBlockAppUser";
    public const string GetAppUserById = "GetAppUserById/{id}";
    public const string GetAppUserBy = "GetAppUserBy";
    public const string GetBy = "GetBy";
    public const string GetByAppUser = "GetByAppUser";
    public const string Delete = "Delete/{id}";
    public const string Update = "Update";
    public const string Create = "Create";
    public const string Approve = "Approve";
    

}
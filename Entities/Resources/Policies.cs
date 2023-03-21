using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace TicketingSystemIT.Entities.Resources;

public class Policies
{
    public const string Supervisor = "Supervisor";
    public const string ITEmployee = "ITEmployee";
    public const string Employee = "Employee";
    public static AuthorizationPolicy Policy(string role)
    {
        var policy = new AuthorizationPolicyBuilder();
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser().RequireRole(role);

        return policy.Build();
    }
}

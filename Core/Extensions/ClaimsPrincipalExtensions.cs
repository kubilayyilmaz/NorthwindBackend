using System;
using System.Security.Claims;

namespace Core.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static List<string> Claims(this ClaimsPrincipal principal, string claimType)
    {
        var result = principal?.FindAll(claimType)?.Select(x => x.Value).ToList();

        return result;
    } 

    public  static List<string> ClaimRoles(this ClaimsPrincipal principal)
    {
        return principal?.Claims(ClaimTypes.Role);
    }
}

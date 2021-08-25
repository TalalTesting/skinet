using System.Linq;
using System.Security.Claims;

namespace Skinet.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user?.FindFirstValue(ClaimTypes.Email);
        }
    }
}

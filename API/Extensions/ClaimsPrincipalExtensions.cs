using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst("email")?.Value;
        }

        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirst("id")?.Value);
        }
    }
}
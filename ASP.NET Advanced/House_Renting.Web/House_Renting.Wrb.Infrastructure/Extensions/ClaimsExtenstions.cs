namespace House_Renting.Web.Infrastructure.Extensions
{
    using System.Security.Claims;

    public static class ClaimsExtenstions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}

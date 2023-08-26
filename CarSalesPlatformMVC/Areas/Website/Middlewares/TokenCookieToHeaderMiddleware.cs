using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarSalesPlatformMVC.Areas.Website.Middlewares
{
    public class TokenCookieToHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenCookieToHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var tokenCookie = context.Request.Cookies["UserToken"];
            if (!string.IsNullOrEmpty(tokenCookie))
            {
                context.Request.Headers.Append("Authorization", $"Bearer {tokenCookie}");
                context.Items["UserIsAuthenticated"] = true;

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(tokenCookie);

                var nameSurnameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "NameSurname");
                if (nameSurnameClaim != null)
                {
                    context.Items["UserNameSurname"] = nameSurnameClaim.Value;
                }

                // UserId'yi HttpContext.Items'a ekliyoruz
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    context.Items["UserId"] = Guid.Parse(userIdClaim.Value);
                }
            }
            else
            {
                context.Items["UserIsAuthenticated"] = false;
            }

            await _next(context);
        }
    }

}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CarCareAlliance.Application.Common.Interfaces.Persistance.AuthRepositories;
using System.Security.Claims;

namespace CarCareAlliance.Infrastructure.Filters.Auth
{
    public class JwtAuthorizeFilter(
        IJwtTokenGenerator jwtTokenGenerator) : IAuthorizationFilter
    {
        private readonly IJwtTokenGenerator jwtTokenGenerator = jwtTokenGenerator;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasAuthorizeAttribute = context.ActionDescriptor.EndpointMetadata
                .Any(a => a.GetType() == typeof(AuthorizeAttribute));

            if (hasAuthorizeAttribute)
            {
                var token = context.HttpContext.Request.Headers
                    .Authorization.ToString().Replace("Bearer ", "");

                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        var claimsPrincipal = jwtTokenGenerator.ValidateToken(token);
                        
                        var authId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        context.HttpContext.Items["AuthId"] = authId;
                    }
                    catch (Exception)
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
                else
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }

    }
}
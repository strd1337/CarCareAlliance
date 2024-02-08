using CarCareAlliance.Application.Common.Interfaces.Persistance.AuthRepositories;
using CarCareAlliance.Application.Common.Services;
using CarCareAlliance.Domain.AuthenticationAggregate;
using CarCareAlliance.Domain.UserProfileAggregate;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarCareAlliance.Infrastructure.Persistance.Repositories.Auth
{
    public class JwtTokenGenerator(
        IDateTimeProvider dateTimeProvider,
        IOptions<JwtSettings> jwtOptions) : IJwtTokenGenerator
    {
        private readonly IDateTimeProvider dateTimeProvider = dateTimeProvider;
        private readonly JwtSettings jwtSettings = jwtOptions.Value;

        public string GenerateToken(Authentication authUser, UserProfile user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, authUser.Id.Value.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, authUser.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(CustomClaims.Roles,
                    Enum.GetName(typeof(RoleType), user.RoleType)!)
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: dateTimeProvider.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.Secret))
                    }, out SecurityToken validatedToken);
                
                return claimsPrincipal;
            }
            catch (SecurityTokenExpiredException)
            {
                throw new ApplicationException("Token has expired.");
            }
        }
    }
}
using CarCareAlliance.Application.Common.Interfaces.Persistance.AuthRepositories;
using CarCareAlliance.Domain.AuthenticationAggregate;
using CarCareAlliance.Domain.AuthenticationAggregate.ValueObjects;
using CarCareAlliance.Infrastructure.Persistance.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace CarCareAlliance.Infrastructure.Persistance.Repositories.Auth
{
    public class AuthRepository(CarCareAllianceDbContext dbContext) :
        GenericRepository<Authentication, AuthenticationId>(dbContext),
        IAuthRepository
    {
        public async Task<bool> IsEmailNotUniqueAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            return await dbContext.RegisteredUsers
                .AnyAsync(u => u.Email.Equals(email), cancellationToken);
        }

        public async Task<bool> IsUsernameNotUniqueAsync(
            string userName,
            CancellationToken cancellationToken = default)
        {
            return await dbContext.RegisteredUsers
               .AnyAsync(u => u.UserName.Equals(userName), cancellationToken);
        }
    }
}
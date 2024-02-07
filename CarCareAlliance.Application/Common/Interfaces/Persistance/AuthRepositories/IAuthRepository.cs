using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Domain.AuthenticationAggregate;
using CarCareAlliance.Domain.AuthenticationAggregate.ValueObjects;

namespace CarCareAlliance.Application.Common.Interfaces.Persistance.AuthRepositories
{
    public interface IAuthRepository
        : IGenericRepository<Authentication, AuthenticationId>
    {
        Task<bool> IsUsernameNotUniqueAsync(string userName,
            CancellationToken cancellationToken = default);

        Task<bool> IsEmailNotUniqueAsync(string email,
            CancellationToken cancellationToken = default);
    }
}
using CarCareAlliance.Presentation.Client.Models.Users;

namespace CarCareAlliance.Presentation.Client.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> UpdateAsync(User user);
        Task<User> GetAsync(string userId);
    }
}

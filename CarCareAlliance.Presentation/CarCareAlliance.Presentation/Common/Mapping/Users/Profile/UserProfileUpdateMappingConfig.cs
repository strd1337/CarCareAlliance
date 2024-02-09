using CarCareAlliance.Application.Users.Common;
using CarCareAlliance.Contracts.Users.UpdateProfile;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.Users.Profile
{
    public class UserProfileUpdateMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserProfileUpdateResult, UserProfileUpdateResponse>()
                .Map(dest => dest, src => src.Information);
        }
    }
}

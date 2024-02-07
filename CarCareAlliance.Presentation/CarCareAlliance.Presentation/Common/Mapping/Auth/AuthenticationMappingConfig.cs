using CarCareAlliance.Application.Auth.Common;
using CarCareAlliance.Contracts.Authentication;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.Auth
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.UserProfileId, src => src.AuthUser.UserProfileId.Value)
                .Map(dest => dest, src => src.AuthUser);
        }
    }
}

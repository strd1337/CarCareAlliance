using CarCareAlliance.Application.Users.Common;
using CarCareAlliance.Contracts.Users.GetProfile;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.Users.Profile
{
    public class UserProfileGetMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserProfileGetResult, UserProfileGetResponse>()
                .Map(dest => dest.UserId, src => src.AuthUser.UserProfileId.Value)
                .Map(dest => dest.Email, src => src.AuthUser.Email)
                .Map(dest => dest.UserName, src => src.AuthUser.UserName)
                .Map(dest => dest.FirstName, src => src.User.Information.FirstName ?? "")
                .Map(dest => dest.LastName, src => src.User.Information.LastName ?? "")
                .Map(dest => dest.PhoneNumber, src => src.User.Information.PhoneNumber ?? "")
                .Map(dest => dest.Address, src => src.User.Information.Address ?? "")
                .Map(dest => dest.City, src => src.User.Information.City ?? "")
                .Map(dest => dest.PostCode, src => src.User.Information.PostCode ?? "")
                .Map(dest => dest.Country, src => src.User.Information.Country ?? "");
        }
    }
}
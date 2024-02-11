﻿using CarCareAlliance.Application.ServicePartners.Commands.Add;
using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Contracts.ServicePartners.AddServicePartner;
using CarCareAlliance.Contracts.ServicePartners.Common;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.ServicePartners
{
    public class ServicePartnerAddMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ServicePartnerAddRequest, ServicePartnerAddCommand>()
                .Map(dest => dest.Latitude, src => src.Location.Latitude)
                .Map(dest => dest.Longitude, src => src.Location.Longitude)
                .Map(dest => dest.Address, src => src.Location.Address)
                .Map(dest => dest.City, src => src.Location.City)
                .Map(dest => dest.State, src => src.Location.State)
                .Map(dest => dest.Country, src => src.Location.Country)
                .Map(dest => dest.PostalCode, src => src.Location.PostalCode)
                .Map(dest => dest.LocationDescription, src => src.Location.Description);

            config.NewConfig<ServicePartnerAddResult, ServicePartnerAddResponse>()
                .Map(dest => dest.ServicePartner, src => new ServicePartnerDto(
                    src.ServicePartner.Id.Value,
                    src.ServicePartner.Name,
                    src.ServicePartner.Description,
                    new ServicePartnerLocationDto(
                        src.ServicePartner.ServiceLocation.Latitude,
                        src.ServicePartner.ServiceLocation.Longitude,
                        src.ServicePartner.ServiceLocation.Address,
                        src.ServicePartner.ServiceLocation.City,
                        src.ServicePartner.ServiceLocation.Country,
                        src.ServicePartner.ServiceLocation.PostalCode,
                        src.ServicePartner.ServiceLocation.Description,
                        src.ServicePartner.ServiceLocation.State ?? "")));
        }
    }
}
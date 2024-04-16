using CarCareAlliance.Presentation.Client.Services.Interfaces;
using LeafletForBlazor;
using Microsoft.AspNetCore.Components;
using CarCareAlliance.Presentation.Client.Common.Constants;

namespace CarCareAlliance.Presentation.Client.Components.Pages.Main
{
    public partial class ServicePartnersMap
    {
        private RealTimeMap? RealTimeMap;

        private RealTimeMap.LoadParameters Parameters = new()
        {
            location = new RealTimeMap.Location()
            {
                latitude = 47.003670,
                longitude = 28.907089,
            },
            zoom_level = 12,
            map_scale = new RealTimeMap.MapScale()
            {
                has = false,
                meters = true
            }
        };

        [Inject]
        public IServicePartnerService? ServicePartnerService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var response = await ServicePartnerService!.GetAllAsync();

            foreach (var servicePartner in response.ServicePartners)
            {
                var latitude = servicePartner.Location.Latitude;
                var longitude = servicePartner.Location.Longitude;

                await RealTimeMap.Geometric.Points.add(new RealTimeMap.StreamPoint
                {
                    latitude = latitude,
                    longitude = longitude,
                    guid = servicePartner.ServicePartnerId,
                    value = servicePartner.Name,
                    type = Constants.ServicePartner.Type
                });

                RealTimeMap.Geometric.Points.Appearance().pattern = new RealTimeMap.PointTooltip()
                {
                    content = $"<b>{servicePartner.Name}</b>",
                    opacity = 0.8,
                    permanent = false
                };
            }

            await base.OnInitializedAsync();
        }
    }
}

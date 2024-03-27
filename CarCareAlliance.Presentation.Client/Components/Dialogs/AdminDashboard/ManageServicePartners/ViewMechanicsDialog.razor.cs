using CarCareAlliance.Presentation.Client.Models.Mechanics;
using CarCareAlliance.Presentation.Client.Models.ServicePartners;
using Microsoft.AspNetCore.Components;

namespace CarCareAlliance.Presentation.Client.Components.Dialogs.AdminDashboard.ManageServicePartners
{
    public partial class ViewMechanicsDialog
    {
        [EditorRequired][Parameter] public ServicePartner Model { get; set; } = default!;
        
        private MechanicProfile? selectedMechanic;
    }
}

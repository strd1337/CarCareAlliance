using CarCareAlliance.Presentation.Client.Models.ServicePartners;
using Microsoft.AspNetCore.Components;

namespace CarCareAlliance.Presentation.Client.Components.Dialogs.AdminDashboard.ManageServicePartners
{
    public partial class ViewServiceCategoriesDialog
    {
        [EditorRequired][Parameter] public ServicePartner Model { get; set; } = default!;

        private ServiceCategory? selectedCategory;
        private Service? selectedService;
    }
}

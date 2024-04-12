using CarCareAlliance.Presentation.Client.Common.Constants;
using CarCareAlliance.Presentation.Client.Models.ServicePartners;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Components.Dialogs.AdminDashboard.ManageServicePartners
{
    public partial class AddServiceDialog
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [EditorRequired][Parameter] public ServicePartner Model { get; set; } = default!;
        [Parameter] public Func<Task>? Refresh { get; set; }

        [Inject]
        public IServicePartnerService? ServicePartnerService { get; set; }
        
        private List<ServiceCategory> TotalServiceCategories { get; set; } = [];
        private List<ServiceCategory> LoadedServiceCategories { get; set; } = [];
        private IEnumerable<Service> ExistingSelectedServices { get; set; } = [];

        private ServiceCategory? existingSelectedCategory = default;
        private Service? existingSelectedService = default;
        private ServiceCategory? selectedCategory = default;
        private Service? selectedService = default;
        private ServiceCategory newSelectedCategory = new();
        private Service newSelectedService = new();
        private MudForm? form;
        private bool isValid;
        private bool existingServicesAreLoad;

        private void Cancel() => MudDialog.Cancel();

        protected override async Task OnInitializedAsync()
        {
            TotalServiceCategories = [.. Model.ServiceCategories];

            await base.OnInitializedAsync();
        }

        private async Task Save()
        {
            Model.ServiceCategories = TotalServiceCategories;

            var isSuccess = await ServicePartnerService!.UpdateAsync(Model);

            if (isSuccess)
            {
                MudDialog.Close(DialogResult.Ok(true));
            }
        }

        private async Task AddAsync()
        {
            await form!.Validate().ConfigureAwait(false);

            if (!form!.IsValid)
            {
                return;
            }

            var category = TotalServiceCategories.FirstOrDefault(x => x.Name
                .Equals(newSelectedCategory.Name, StringComparison.CurrentCultureIgnoreCase));

            if (category is null) 
            {
                newSelectedCategory.Services = [];
                newSelectedCategory.Services.Add(newSelectedService);

                TotalServiceCategories.Add(newSelectedCategory);
            }
            else
            {
                TotalServiceCategories.FirstOrDefault(x => x.Name.Equals(newSelectedCategory.Name, StringComparison.CurrentCultureIgnoreCase))!
                    .Services.Add(newSelectedService);
            }

            Snackbar.Add(Constants.AddSuccessfulConfirmation(newSelectedService.Name), Severity.Success);

            newSelectedCategory = new();
            newSelectedService = new();

            selectedCategory = null;
            selectedService = null;
        }

        private string GetServiceNames(List<string> selectedValues)
        {
            return $"Selected service{(ExistingSelectedServices.Count() > 1 ? "s" : "")}: {string.Join(", ", ExistingSelectedServices.Select(x => x.Name))}";
        }
        
        private async Task AddSelectedServicesAsync()
        {
            List<string> names = [];

            foreach (var selectedService in ExistingSelectedServices)
            {
                var selectedCategory = LoadedServiceCategories.FirstOrDefault(category =>
                    category.Services.Any(service => service.ServiceId == selectedService.ServiceId));

                if (selectedCategory is not null)
                {
                    var existingCategory = TotalServiceCategories.FirstOrDefault(category =>
                        category.Name.Equals(selectedCategory.Name, StringComparison.OrdinalIgnoreCase));

                    if (existingCategory is not null)
                    {
                        existingCategory.Services.Add(selectedService);
                    }
                    else
                    {
                        var newCategory = new ServiceCategory
                        {
                            ServiceCategoryId = selectedCategory.ServiceCategoryId,
                            Name = selectedCategory.Name,
                            Description = selectedCategory.Description,
                            Services = new List<Service> { selectedService }
                        };

                        TotalServiceCategories.Add(newCategory);
                    }

                    names.Add(selectedService.Name);
                }
            }

            Snackbar.Add(Constants.AddSuccessfulConfirmantion([.. names]), Severity.Success);

            LoadedServiceCategories = [];
            ExistingSelectedServices = [];
            existingSelectedCategory = null;
            existingSelectedService = null;
            existingServicesAreLoad = false;
            selectedCategory = null;
            selectedService = null;

            await LoadExistingServicesAndResetIfIndexChanged();

        }
        
        private async Task LoadExistingServicesAndResetIfIndexChanged()
        {
            if (!existingServicesAreLoad)
            {
                var response = await ServicePartnerService!.GetAllServiecCategoriesAsync();

                LoadedServiceCategories = response.ServiceCategories
                    .Where(category =>
                        !TotalServiceCategories.Any(totalCategory => totalCategory.ServiceCategoryId == category.ServiceCategoryId))
                    .Select(category =>
                    {
                        var totalCategory = TotalServiceCategories
                            .FirstOrDefault(totalCategory => totalCategory.Name.Equals(category.Name, StringComparison.CurrentCultureIgnoreCase));

                        if (totalCategory is not null)
                        {
                            var newServices = category.Services
                                .Where(service =>
                                    !totalCategory.Services.Any(totalService =>
                                        totalService.Name.Equals(service.Name, StringComparison.CurrentCultureIgnoreCase)))
                                .ToList();

                            return new ServiceCategory
                            {
                                ServiceCategoryId = category.ServiceCategoryId,
                                Name = category.Name,
                                Description = category.Description,
                                Services = newServices
                            };
                        }
                        else
                        {
                            return category;
                        }
                    })
                    .Where(category => category.Services.Any())
                    .ToList();

                existingServicesAreLoad = true;
            }
            
            ExistingSelectedServices = [];
            existingSelectedCategory = null;
            existingSelectedService = null;
            newSelectedCategory = new();
            newSelectedService = new();
            StateHasChanged();
        }
    }
}

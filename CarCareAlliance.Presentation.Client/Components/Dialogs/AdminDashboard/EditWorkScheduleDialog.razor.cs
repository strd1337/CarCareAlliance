using CarCareAlliance.Presentation.Client.Models.WorkSchedules;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Components.Dialogs.AdminDashboard
{
    public partial class EditWorkScheduleDialog
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [EditorRequired][Parameter] public ICollection<WorkSchedule> WorkSchedules { get; set; } = default!;
        [Parameter] public Guid OwnerId { get; set; }
        [Parameter] public Func<Task>? Refresh { get; set; }

        [Inject]
        public IWorkScheduleService? WorkScheduleService { get; set; }

        private MudForm? form;
        private WorkSchedule SelectedSchedule { get; set; } = default!;
        private BreakTime? SelectedBreakTime { get; set; }
        private IEnumerable<DayOfWeek> Weekends { get; set; } = default!;

        private bool isValid;
        private int currentIndex;

        private void Cancel() => MudDialog.Cancel();

        protected override void OnInitialized()
        {
            Weekends = FindMissingDays(WorkSchedules.ToList());

            base.OnInitialized();
        }

        private async Task Save()
        {
            await form!.Validate().ConfigureAwait(false);

            if (!form!.IsValid)
            {
                return;
            }

            var isSuccess = await WorkScheduleService!.UpdateByOwnerIdAsync(OwnerId, WorkSchedules);

            if (isSuccess)
            {
                MudDialog.Close(DialogResult.Ok(true));
            }
        }

        private static IEnumerable<DayOfWeek> FindMissingDays(
            IEnumerable<WorkSchedule> workSchedules)
        {
            var allDays = workSchedules
                .OrderBy(x => x.DayOfWeek)
                .Select(ws => ws.DayOfWeek)
                .Distinct()
                .ToList();

            var allDaysOfWeek = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();

            var missingDays = allDaysOfWeek.Except(allDays);

            return missingDays;
        }
    }
}
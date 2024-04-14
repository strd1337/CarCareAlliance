using CarCareAlliance.Presentation.Client.Common.Attributes;
using CarCareAlliance.Presentation.Client.Common.Convertors;
using CarCareAlliance.Presentation.Client.Models.WorkSchedules;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace CarCareAlliance.Presentation.Client.Components.Dialogs.AdminDashboard
{
    public partial class AddWorkScheduleDialog
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [EditorRequired][Parameter] public Guid Owner { get; set; } = default!;
        [Parameter] public Func<Task>? Refresh { get; set; }

        [Inject]
        public IWorkScheduleService? WorkScheduleService { get; set; }

        private MudForm? form;

        private WorkSchedule WorkSchedule { get; set; } = new()
        {
            StartTime = TimeConverter.TimeSpanToTimeOnly(TimeSpan.Zero),
            EndTime = TimeConverter.TimeSpanToTimeOnly(TimeSpan.Zero)
        };

        private List<BreakTime> BreakTimes { get; set; } = [];
        private TimeSpan BreakStartTime { get; set; } = TimeSpan.Zero;
        private TimeSpan BreakEndTime { get; set; } = TimeSpan.Zero;

        private int quantity = 1;
        private int currentStep = 1;
        private bool isValid;
        private MudTimePicker? breakStartTimePicker;
        private bool breakTimeIsShown = true;

        private void Cancel() => MudDialog.Cancel();

        protected override void OnInitialized()
        {
            BreakTimes.Add(new BreakTime
            {
                StartTime = TimeConverter.TimeSpanToTimeOnly(BreakStartTime),
                EndTime = TimeConverter.TimeSpanToTimeOnly(BreakEndTime)
            });

            base.OnInitialized();
        }

        private async Task Add()
        {
            await form!.Validate().ConfigureAwait(false);

            if (!form!.IsValid)
            {
                return;
            }

            WorkSchedule.OwnerId = Owner;
            WorkSchedule.BreakTimes = BreakTimes;

            var isSuccess = await WorkScheduleService!.CreateAsync(WorkSchedule);

            if (isSuccess)
            {
                MudDialog.Close(DialogResult.Ok(true));
            }
        }

        private void NextStep()
        {
            currentStep++;
            breakTimeIsShown = true;
            StateHasChanged();
        }

        private void PreviousStep()
        {
            if (currentStep > 1)
            {
                currentStep--;
                breakTimeIsShown = true;
                StateHasChanged();
            }
        }

        private void ResetValidation()
        {
            if (breakTimeIsShown)
            {
                breakStartTimePicker?.ResetValidation();
                breakTimeIsShown = false;
                StateHasChanged();
            }
        }

        private void HandleQuantityChange()
        {
            currentStep = 1;

            BreakTimes = [];
            for (int i = 0; i < quantity; i++)
            {
                BreakTimes.Add(new BreakTime
                {
                    StartTime = TimeConverter.TimeSpanToTimeOnly(TimeSpan.Zero),
                    EndTime = TimeConverter.TimeSpanToTimeOnly(TimeSpan.Zero)
                });
            }
        }

        private void SetBreakTimeStartTime(int index, TimeSpan time)
        {
            if (index >= 0 && index < BreakTimes.Count)
            {
                BreakTimes[index].StartTime = TimeConverter.TimeSpanToTimeOnly(time);
            }
        }

        private void SetBreakTimeEndTime(int index, TimeSpan time)
        {
            if (index >= 0 && index < BreakTimes.Count)
            {
                BreakTimes[index].EndTime = TimeConverter.TimeSpanToTimeOnly(time);
            }
        }

        private BreakTime GetBreakTime(int index)
        {
            if (index >= 0 && index < BreakTimes.Count)
            {
                return BreakTimes[index];
            }
            return new BreakTime();
        }

        private ValidationAttribute GetStartTimeValidationAttribute()
        {
            double min = currentStep - 2 == -1 ?
                TimeConverter.TimeOnlyToTimeSpan(WorkSchedule.StartTime).TotalSeconds :
                TimeConverter.TimeOnlyToTimeSpan(GetBreakTime(currentStep - 2).EndTime).TotalSeconds;

            double max = TimeConverter.TimeOnlyToTimeSpan(WorkSchedule.EndTime).TotalSeconds;

            TimeSpan minTime = currentStep - 2 == -1 ?
                TimeConverter.TimeOnlyToTimeSpan(WorkSchedule.StartTime) :
                TimeConverter.TimeOnlyToTimeSpan(GetBreakTime(currentStep - 2).EndTime);

            TimeSpan maxTime = TimeConverter.TimeOnlyToTimeSpan(WorkSchedule.EndTime);

            return new TimeSpanRangeAttribute(min, max)
            {
                ErrorMessage = $"Break start time should be between {minTime} and {maxTime}!"
            };
        }

        private ValidationAttribute GetEndTimeValidationAttribute()
        {
            double min = TimeConverter.TimeOnlyToTimeSpan(BreakTimes[currentStep - 1].StartTime).TotalSeconds;

            double max = TimeConverter.TimeOnlyToTimeSpan(WorkSchedule.EndTime).TotalSeconds;

            TimeSpan minTime = TimeConverter.TimeOnlyToTimeSpan(BreakTimes[currentStep - 1].StartTime);

            TimeSpan maxTime = TimeConverter.TimeOnlyToTimeSpan(WorkSchedule.EndTime);

            return new TimeSpanRangeAttribute(min, max)
            {
                ErrorMessage = $"Break end time should be between {minTime} and {maxTime}!"
            };
        }
    }
}
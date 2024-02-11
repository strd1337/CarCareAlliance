using CarCareAlliance.Application.WorkSchedules.Commands.Add;
using CarCareAlliance.Application.WorkSchedules.Common;
using CarCareAlliance.Contracts.WorkSchedules.AddWorkSchedule;
using CarCareAlliance.Contracts.WorkSchedules.Common;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.WorkSchedule
{
    public class WorkScheduleAddMappingConfig
        : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<WorkScheduleAddRequest, WorkScheduleAddCommand>()
                .Map(dest => dest.BreakTimes, src => src.BreakTimes
                    .Select(breakTime => BreakTime.CreateNew(
                        breakTime.StartTime,
                        breakTime.EndTime)).ToList());

            config.NewConfig<WorkScheduleAddResult, WorkScheduleAddResponse>()
                .Map(dest => dest.WorkSchedule, src => new WorkScheduleDto(
                    src.WorkSchedule.Id.Value,
                    src.WorkSchedule.DayOfWeek,
                    src.WorkSchedule.StartTime,
                    src.WorkSchedule.EndTime,
                    src.WorkSchedule.OwnerId,
                    src.WorkSchedule.Weekends.ToList(),
                    src.WorkSchedule.BreakTimes
                        .Select(breakTime => 
                            new BreakTimeDto(
                                breakTime.StartTime,
                                breakTime.EndTime)).ToList()));       
        }
    }
}
using CarCareAlliance.Application.WorkSchedules.Common;
using CarCareAlliance.Contracts.WorkSchedules.Common;
using CarCareAlliance.Contracts.WorkSchedules.GetByOwnerId;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.WorkSchedule
{
    public class WorkScheduleGetByOwnerIdMappingConfig
        : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<WorkScheduleGetByOwnerIdResult, WorkScheduleGetByOwnerIdResponse>()
                .Map(dest => dest.WorkSchedule, src => new WorkScheduleDto(
                    src.WorkSchedule.Id.Value,
                    src.WorkSchedule.DayOfWeek,
                    src.WorkSchedule.StartTime,
                    src.WorkSchedule.EndTime,
                    src.WorkSchedule.OwnerId,
                    src.WorkSchedule.BreakTimes
                        .Select(breakTime =>
                            new BreakTimeDto(
                                breakTime.StartTime,
                                breakTime.EndTime)).ToList()));
        }
    }
}

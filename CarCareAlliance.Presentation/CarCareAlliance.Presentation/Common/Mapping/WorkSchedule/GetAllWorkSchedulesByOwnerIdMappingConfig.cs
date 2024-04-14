using CarCareAlliance.Application.WorkSchedules.Common;
using CarCareAlliance.Contracts.WorkSchedules.Common;
using CarCareAlliance.Contracts.WorkSchedules.GetAllByOwnerId;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.WorkSchedule
{
    public class GetAllWorkSchedulesByOwnerIdMappingConfig
        : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetAllWorkSchedulesByOwnerIdResult, GetAllWorkSchedulesByOwnerIdResponse>()
                .Map(dest => dest.WorkSchedules, src => src.WorkSchedules.Select(workSchedule => 
                    new WorkScheduleDto(
                        workSchedule.Id.Value,
                        workSchedule.DayOfWeek,
                        workSchedule.StartTime,
                        workSchedule.EndTime,
                        workSchedule.OwnerId,
                        workSchedule.BreakTimes.Select(breakTime => 
                            new BreakTimeDto(
                                breakTime.StartTime,
                                breakTime.EndTime)).ToList())));
        }
    }
}

using CarCareAlliance.Application.WorkSchedules.Commands.Update;
using CarCareAlliance.Application.WorkSchedules.Common;
using CarCareAlliance.Contracts.WorkSchedules.UpdateByOwnerId;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.WorkSchedule
{
    public class UpdateWorkSchedulesByOwnerIdMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UpdateWorkSchedulesByOwnerIdRequest, UpdateWorkSchedulesByOwnerIdCommand>();
             
            config.NewConfig<UpdateWorkSchedulesByOwnerIdResult, UpdateWorkSchedulesByOwnerIdResponse>();
        }
    }
}

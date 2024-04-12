using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Domain.MechanicAggregate;
using CarCareAlliance.Domain.MechanicAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.WorkScheduleAggregate;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;
using ErrorOr;

namespace CarCareAlliance.Application.ServicePartners.Queries.GetAll
{
    public class ServicePartnerGetAllHandler(IUnitOfWork unitOfWork) 
        : IQueryHandler<ServicePartnerGetAllQuery, ServicePartnerGetAllResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<ServicePartnerGetAllResult>> Handle(
            ServicePartnerGetAllQuery query,
            CancellationToken cancellationToken)
        {
            var servicePartners = unitOfWork
                .GetRepository<ServicePartner, ServicePartnerId>()
                .GetAll();

            var result = new List<ServicePartnerResult>();
            foreach (var servicePartner in servicePartners)
            {
                var mechanicProfileIds = servicePartner.MechanicProfileIds
                    .Select(id => MechanicProfileId.Create(id.Value))
                    .ToList();

                var mechanics = unitOfWork
                    .GetRepository<MechanicProfile, MechanicProfileId>()
                    .GetWhere(x => mechanicProfileIds.Contains(x.Id)).ToList();

                var workSchedules = unitOfWork
                    .GetRepository<WorkSchedule, WorkScheduleId>()
                    .GetWhere(x => x.OwnerId == servicePartner.Id.Value);

                var userProfileIds = mechanics
                    .Select(mechanic => MechanicProfileId.Create(mechanic.UserProfileId.Value))
                    .ToList();

                var mechanicProfiles = unitOfWork
                    .GetRepository<UserProfile, UserProfileId>()
                    .GetWhere(x => userProfileIds.Contains(x.Id));

                result.Add(new ServicePartnerResult
                {
                    ServicePartner = servicePartner,
                    Mechanics = mechanics,
                    WorkSchedules = workSchedules.ToList(),
                    MechanicProfiles = mechanicProfiles.ToList()
                });
            }

            return new ServicePartnerGetAllResult(result);
        }
    }
}
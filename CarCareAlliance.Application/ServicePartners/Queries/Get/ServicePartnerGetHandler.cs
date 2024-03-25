using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using ErrorOr;
using CarCareAlliance.Domain.Common.Errors;
using CarCareAlliance.Domain.MechanicAggregate;
using CarCareAlliance.Domain.MechanicAggregate.ValueObjects;
using CarCareAlliance.Domain.WorkScheduleAggregate;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;

namespace CarCareAlliance.Application.ServicePartners.Queries.Get
{
    public class ServicePartnerGetHandler(IUnitOfWork unitOfWork)
        : IQueryHandler<ServicePartnerGetQuery, ServicePartnerResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<ServicePartnerResult>> Handle(
            ServicePartnerGetQuery query,
            CancellationToken cancellationToken)
        {
            var servicePartner = await unitOfWork
                .GetRepository<ServicePartner, ServicePartnerId>()
                .GetByIdAsync(
                    ServicePartnerId.Create(query.ServicePartnerId),
                    cancellationToken);

            if (servicePartner is null)
            {
                return Errors.ServicePartner.NotFound;
            }

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

            return new ServicePartnerResult
            {
                ServicePartner = servicePartner,
                Mechanics = mechanics,
                WorkSchedules = workSchedules.ToList(),
                MechanicProfiles = mechanicProfiles.ToList()
            };
        }
    }
}
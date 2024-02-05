using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.ServiceHistoryAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.TicketAggregate.ValueObjects;

namespace CarCareAlliance.Domain.ServiceHistoryAggregate
{
    public sealed class ServiceHistory : AggregateRoot<ServiceHistoryId, Guid>
    {
        private readonly List<TicketId> ticketIds = [];

        public ServicePartnerId ServicePartnerId { get; private set; }
        
        public IReadOnlyList<TicketId> TicketIds => ticketIds.AsReadOnly();

        private ServiceHistory(
            ServiceHistoryId id,
            ServicePartnerId servicePartnerId) : base(id)
        {
            ServicePartnerId = servicePartnerId;
        }

        public static ServiceHistory Create(
            ServicePartnerId servicePartnerId)
        {
            return new ServiceHistory(
                ServiceHistoryId.CreateUnique(),
                servicePartnerId);
        }

#pragma warning disable CS8618
        private ServiceHistory()
        {
        }
#pragma warning restore CS8618
    }
}

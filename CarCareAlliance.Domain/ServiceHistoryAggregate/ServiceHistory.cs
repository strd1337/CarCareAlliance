using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.ServiceHistoryAggregate.Entities;
using CarCareAlliance.Domain.ServiceHistoryAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;

namespace CarCareAlliance.Domain.ServiceHistoryAggregate
{
    public sealed class ServiceHistory : AggregateRoot<ServiceHistoryId, Guid>
    {
        private readonly List<Ticket> tickets = [];

        public ServicePartnerId ServicePartnerId { get; private set; }
        
        public IReadOnlyList<Ticket> Tickets => tickets.AsReadOnly();

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

        public void AddTicket(Ticket ticket)
        {
            tickets.Add(ticket);
        }

#pragma warning disable CS8618
        private ServiceHistory()
        {
        }
#pragma warning restore CS8618
    }
}

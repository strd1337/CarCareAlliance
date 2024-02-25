using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.MechanicAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.SparePartAggregate.ValueObjects;
using CarCareAlliance.Domain.TicketAggregate.ValueObjects;

namespace CarCareAlliance.Domain.TicketAggregate.Entities
{
    public sealed class OrderDetails : Entity<OrderDetailsId>
    {
        private readonly List<SparePartId> sparePartIds = [];
        private readonly List<ServiceId> serviceIds = [];

        public float Mileage { get; private set; }
        public string Comments { get; private set; }
        public float FinalPrice { get; private set; }
        public float PrepaymentAmount { get; private set; }
        public DateTime? PaymentDueDate { get; private set; }
        public MechanicProfileId? AssignedMechanicId { get; private set; }

        public IReadOnlyList<SparePartId> SparePartIds => sparePartIds.AsReadOnly();
        public IReadOnlyList<ServiceId> ServiceIds => serviceIds.AsReadOnly();

        private OrderDetails(
            OrderDetailsId id,
            float mileage,
            string comments,
            float finalPrice,
            float prepaymentAmount) : base(id)
        {
            Mileage = mileage;
            Comments = comments;
            FinalPrice = finalPrice;
            PrepaymentAmount = prepaymentAmount;
        }

        public static OrderDetails Create(
            float mileage,
            string comments,
            float finalPrice,
            float prepaymentAmount)
        {
            return new(
                OrderDetailsId.CreateUnique(),
                mileage,
                comments,
                finalPrice,
                prepaymentAmount);
        }

        public void UpdatePaymentDueDate(DateTime paymentDueDate)
        {
            PaymentDueDate = paymentDueDate;
        }

        public void UpdateAssignedMechanic(MechanicProfileId assignedMechanicId)
        {
            AssignedMechanicId = assignedMechanicId;
        }

        public void UpdateFinalPrice(float finalPrice)
        {
            FinalPrice = finalPrice;
        }

        public void AddService(ServiceId serviceId)
        {
            serviceIds.Add(serviceId);
        }

#pragma warning disable CS8618
        private OrderDetails()
        {
        }
#pragma warning restore CS8618
    }
}
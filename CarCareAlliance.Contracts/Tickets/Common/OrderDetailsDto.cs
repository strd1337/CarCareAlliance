namespace CarCareAlliance.Contracts.Tickets.Common
{
    public record OrderDetailsDto(
        Guid OrderDetailsId,
        float Mileage,
        string Comments,
        float FinalPrice,
        float PrepaymentAmount,
        ICollection<Guid> ServiceIds);
}
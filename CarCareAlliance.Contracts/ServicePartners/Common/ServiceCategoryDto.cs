namespace CarCareAlliance.Contracts.ServicePartners.Common
{
    public record ServiceCategoryDto(
        Guid ServiceCategoryId,
        string Name,
        string Description,
        ICollection<ServiceDto> Services);
}

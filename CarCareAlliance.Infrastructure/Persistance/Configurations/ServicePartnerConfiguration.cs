using CarCareAlliance.Domain.MechanicAggregate.ValueObjects;
using CarCareAlliance.Domain.PhotoAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.Entities;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCareAlliance.Infrastructure.Persistance.Configurations
{
    public class ServicePartnerConfiguration
        : IEntityTypeConfiguration<ServicePartner>
    {
        public void Configure(EntityTypeBuilder<ServicePartner> builder)
        {
            ConfigureServicePartnerTable(builder);
            ConfigureServicePartnerPhotoIdsTable(builder);
            ConfigureServicePartnerReviewIdsTable(builder);
            ConfigureServicePartnerMechanicProfileIdsTable(builder);
            ConfigureServicePartnerServiceCategoriesTable(builder);
        }

        private static void ConfigureServicePartnerTable(
           EntityTypeBuilder<ServicePartner> builder)
        {
            builder.ToTable("ServicePartners");

            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ServicePartnerId.Create(value));

            builder.Property(sp => sp.LogoId)
                .HasConversion(
                    id => id.Value,
                    value => PhotoId.Create(value));

            builder.Property(sp => sp.WorkScheduleId)
                .HasConversion(
                    id => id.Value,
                    value => WorkScheduleId.Create(value));

            builder.Property(sp => sp.Name)
                .HasMaxLength(30);

            builder.Property(sp => sp.Description)
                .HasMaxLength(1000);

            builder.OwnsOne(sp => sp.ServiceLocation, slb =>
            {
                slb.HasKey(sl => sl.Id);

                slb.Property(od => od.Id)
                        .HasColumnName("ServiceLocationId")
                        .ValueGeneratedNever()
                        .HasConversion(
                            id => id.Value,
                            value => ServiceLocationId.Create(value));

                slb.Property(sl => sl.Latitude)
                    .HasColumnName("Latitude")
                    .HasColumnType("decimal(10, 6)");

                slb.Property(sl => sl.Longitude)
                    .HasColumnName("Longitude")
                    .HasColumnType("decimal(10, 6)");

                slb.Property(sl => sl.Address)
                    .HasColumnName("Address")
                    .HasMaxLength(255);

                slb.Property(sl => sl.City)
                    .HasColumnName("City")
                    .HasMaxLength(100);

                slb.Property(sl => sl.State)
                    .HasColumnName("State")
                    .HasMaxLength(100);

                slb.Property(sl => sl.Country)
                    .HasColumnName("Country")
                    .HasMaxLength(100);

                slb.Property(sl => sl.PostalCode)
                    .HasColumnName("PostalCode")
                    .HasMaxLength(20);

                slb.Property(sl => sl.Description)
                    .HasColumnName("Description")
                    .HasMaxLength(255);
            });
        }

        private static void ConfigureServicePartnerPhotoIdsTable(
            EntityTypeBuilder<ServicePartner> builder)
        {
            builder.OwnsMany(sp => sp.PhotoIds, pib =>
            {
                pib.ToTable("ServicePartnerPhotoIds");

                pib.WithOwner().HasForeignKey("ServicePartnerId");

                pib.HasKey("Id");

                pib.Property(ni => ni.Value)
                    .HasColumnName("PhotoId")
                    .ValueGeneratedNever();
            });
        }

        private static void ConfigureServicePartnerReviewIdsTable(
            EntityTypeBuilder<ServicePartner> builder)
        {
            builder.OwnsMany(sp => sp.ReviewIds, pib =>
            {
                pib.ToTable("ServicePartnerReviewIds");

                pib.WithOwner().HasForeignKey("ServicePartnerId");

                pib.HasKey("Id");

                pib.Property(ni => ni.Value)
                    .HasColumnName("ReviewId")
                    .ValueGeneratedNever();
            });
        }

        private static void ConfigureServicePartnerMechanicProfileIdsTable(
            EntityTypeBuilder<ServicePartner> builder)
        {
            builder.OwnsMany(sp => sp.MechanicProfileIds, pib =>
            {
                pib.ToTable("ServicePartnerMechanicProfileIds");

                pib.WithOwner().HasForeignKey("ServicePartnerId");

                pib.HasKey("Id");

                pib.Property(ni => ni.Value)
                    .HasColumnName("MechanicProfileId")
                    .ValueGeneratedNever();
            });
        }

        private static void ConfigureServicePartnerServiceCategoriesTable(
             EntityTypeBuilder<ServicePartner> builder)
        {
            builder.OwnsMany(sp => sp.ServiceCategories, scb =>
            {
                scb.ToTable("ServicePartnerServiceCategories");

                scb.WithOwner().HasForeignKey("ServicePartnerId");

                scb.HasKey("Id", "ServicePartnerId");

                scb.Property(sc => sc.Id)
                    .HasColumnName("ServicePartnerServiceCategoryId")
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => ServiceCategoryId.Create(value));

                scb.Property(od => od.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(50);

                scb.Property(od => od.Description)
                    .HasColumnName("Description")
                    .HasMaxLength(50);

                scb.OwnsMany(sc => sc.Services, sb =>
                {
                    sb.ToTable("ServiceCategoryServices");

                    sb.WithOwner().HasForeignKey(
                        "ServicePartnerServiceCategoryId",
                        "ServicePartnerId");

                    sb.HasKey(
                        nameof(Service.Id), 
                        "ServicePartnerServiceCategoryId",
                        "ServicePartnerId");

                    sb.Property(s => s.Id)
                        .HasColumnName("ServiceId")
                        .ValueGeneratedNever()
                        .HasConversion(
                            id => id.Value,
                            value => ServiceId.Create(value));

                    sb.Property(s => s.Name)
                        .HasColumnName("Name")
                        .HasMaxLength(100);

                    sb.Property(s => s.Description)
                        .HasColumnName("Description")
                        .HasMaxLength(255);

                    sb.Property(s => s.Price)
                        .HasColumnName("Price")
                        .HasColumnType("decimal(10, 2)");

                    sb.Property(s => s.Duration)
                        .HasColumnName("Duration")
                        .HasColumnType("decimal(10, 2)");
                });

                scb.Navigation(sc => sc.Services).Metadata.SetField("services");
                scb.Navigation(sc => sc.Services)
                    .UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            builder.Metadata.FindNavigation(nameof(ServicePartner.ServiceCategories))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
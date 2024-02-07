using CarCareAlliance.Domain.AuthenticationAggregate;
using CarCareAlliance.Domain.ExpenseHistoryAggregate;
using CarCareAlliance.Domain.MaintenanceAggregate;
using CarCareAlliance.Domain.MaintenanceAggregate.Entities;
using CarCareAlliance.Domain.MechanicAggregate;
using CarCareAlliance.Domain.NotificationAggregate;
using CarCareAlliance.Domain.PartsCategoryAggregate;
using CarCareAlliance.Domain.PhotoAggregate;
using CarCareAlliance.Domain.RepairAggregate;
using CarCareAlliance.Domain.ReviewAggregate;
using CarCareAlliance.Domain.ServiceHistoryAggregate;
using CarCareAlliance.Domain.ServiceHistoryAggregate.Entities;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.Entities;
using CarCareAlliance.Domain.SparePartAggregate;
using CarCareAlliance.Domain.TicketAggregate.Entities;
using CarCareAlliance.Domain.UserProfileAggregate;
using CarCareAlliance.Domain.VehicleAggregate;
using CarCareAlliance.Domain.WorkScheduleAggregate;
using Microsoft.EntityFrameworkCore;

namespace CarCareAlliance.Infrastructure.Persistance
{
    public class CarCareAllianceDbContext(
        DbContextOptions<CarCareAllianceDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(
                    typeof(CarCareAllianceDbContext).Assembly);

            base.OnModelCreating(modelBuilder);    
        }

        public DbSet<Authentication> RegisteredUsers { get; set; } = null!;
        public DbSet<ExpenseHistory> ExpenseHistories { get; set; } = null!;
        public DbSet<ScheduledMaintenance> ScheduledMaintenances { get; set; } = null!;
        public DbSet<MaintenanceType> MaintenanceTypes { get; set; } = null!;
        public DbSet<MechanicProfile> MechanicProfiles { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<Photo> Photos { get; set; } = null!;
        public DbSet<RepairHistory> RepairHistories { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<ServiceHistory> ServiceHistories { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<OrderDetails> OrderDetailsCollection { get; set; } = null!;
        public DbSet<ServicePartner> ServicePartners { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<ServiceCategory> ServiceCategories { get; set; } = null!;
        public DbSet<ServiceLocation> ServiceLocations { get; set; } = null!;
        public DbSet<SparePart> SpareParts { get; set; } = null!;
        public DbSet<SparePartsCategory> SparePartsCategories { get; set; } = null!;
        public DbSet<UserProfile> UserProfiles { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<WorkSchedule> WorkSchedules { get; set; } = null!;
    }
}
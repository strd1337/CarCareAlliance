using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarCareAlliance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiptImage = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MechanicProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicePartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Experience = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MechanicProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TextMessage = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DateTimeSent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ScheduledMaintenanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailsCollection",
                columns: table => new
                {
                    OrderDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mileage = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PrepaymentAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedMechanicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailsCollection", x => x.OrderDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageData = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false),
                    Format = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateTaken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServicePartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepairHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RepairStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObjectType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    DatePublished = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledMaintenances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledMaintenances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicePartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServicePartners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    LogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorkScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePartners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpareParts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SparePartsCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpareParts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SparePartsCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SparePartsCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Vin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weekends = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MechanicProfileReviewIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MechanicProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MechanicProfileReviewIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MechanicProfileReviewIds_MechanicProfiles_MechanicProfileId",
                        column: x => x.MechanicProfileId,
                        principalTable: "MechanicProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailsServiceIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailsServiceIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetailsServiceIds_OrderDetailsCollection_OrderDetailsId",
                        column: x => x.OrderDetailsId,
                        principalTable: "OrderDetailsCollection",
                        principalColumn: "OrderDetailsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailsSparePartIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SparePartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailsSparePartIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetailsSparePartIds_OrderDetailsCollection_OrderDetailsId",
                        column: x => x.OrderDetailsId,
                        principalTable: "OrderDetailsCollection",
                        principalColumn: "OrderDetailsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairHistoryNotificationIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RepairHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairHistoryNotificationIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairHistoryNotificationIds_RepairHistories_RepairHistoryId",
                        column: x => x.RepairHistoryId,
                        principalTable: "RepairHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledMaintenanceNotificationIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduledMaintenanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledMaintenanceNotificationIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledMaintenanceNotificationIds_ScheduledMaintenances_ScheduledMaintenanceId",
                        column: x => x.ScheduledMaintenanceId,
                        principalTable: "ScheduledMaintenances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledMaintenanceTypes",
                columns: table => new
                {
                    ScheduledMaintenanceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduledMaintenanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledMaintenanceTypes", x => new { x.ScheduledMaintenanceTypeId, x.ScheduledMaintenanceId });
                    table.ForeignKey(
                        name: "FK_ScheduledMaintenanceTypes_ScheduledMaintenances_ScheduledMaintenanceId",
                        column: x => x.ScheduledMaintenanceId,
                        principalTable: "ScheduledMaintenances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceHistoryTickets",
                columns: table => new
                {
                    ServiceHistoryTicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RepairStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceHistoryTickets", x => new { x.ServiceHistoryTicketId, x.ServiceHistoryId });
                    table.ForeignKey(
                        name: "FK_ServiceHistoryTickets_OrderDetailsCollection_OrderDetailsId",
                        column: x => x.OrderDetailsId,
                        principalTable: "OrderDetailsCollection",
                        principalColumn: "OrderDetailsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceHistoryTickets_ServiceHistories_ServiceHistoryId",
                        column: x => x.ServiceHistoryId,
                        principalTable: "ServiceHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceLocations",
                columns: table => new
                {
                    ServiceLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(10,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(10,6)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ServicePartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceLocations", x => x.ServiceLocationId);
                    table.ForeignKey(
                        name: "FK_ServiceLocations_ServicePartners_ServicePartnerId",
                        column: x => x.ServicePartnerId,
                        principalTable: "ServicePartners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePartnerMechanicProfileIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MechanicProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicePartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePartnerMechanicProfileIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicePartnerMechanicProfileIds_ServicePartners_ServicePartnerId",
                        column: x => x.ServicePartnerId,
                        principalTable: "ServicePartners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePartnerPhotoIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicePartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePartnerPhotoIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicePartnerPhotoIds_ServicePartners_ServicePartnerId",
                        column: x => x.ServicePartnerId,
                        principalTable: "ServicePartners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePartnerReviewIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicePartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePartnerReviewIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicePartnerReviewIds_ServicePartners_ServicePartnerId",
                        column: x => x.ServicePartnerId,
                        principalTable: "ServicePartners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePartnerServiceCategories",
                columns: table => new
                {
                    ServicePartnerServiceCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicePartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePartnerServiceCategories", x => new { x.ServicePartnerServiceCategoryId, x.ServicePartnerId });
                    table.ForeignKey(
                        name: "FK_ServicePartnerServiceCategories_ServicePartners_ServicePartnerId",
                        column: x => x.ServicePartnerId,
                        principalTable: "ServicePartners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileReviewIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileReviewIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileReviewIds_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehiclePhotoIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclePhotoIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehiclePhotoIds_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BreakTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    WorkScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreakTimes_WorkSchedules_WorkScheduleId",
                        column: x => x.WorkScheduleId,
                        principalTable: "WorkSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategoryServices",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicePartnerServiceCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicePartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Duration = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategoryServices", x => new { x.ServiceId, x.ServicePartnerServiceCategoryId, x.ServicePartnerId });
                    table.ForeignKey(
                        name: "FK_ServiceCategoryServices_ServicePartnerServiceCategories_ServicePartnerServiceCategoryId_ServicePartnerId",
                        columns: x => new { x.ServicePartnerServiceCategoryId, x.ServicePartnerId },
                        principalTable: "ServicePartnerServiceCategories",
                        principalColumns: new[] { "ServicePartnerServiceCategoryId", "ServicePartnerId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BreakTimes_WorkScheduleId",
                table: "BreakTimes",
                column: "WorkScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_MechanicProfileReviewIds_MechanicProfileId",
                table: "MechanicProfileReviewIds",
                column: "MechanicProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailsServiceIds_OrderDetailsId",
                table: "OrderDetailsServiceIds",
                column: "OrderDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailsSparePartIds_OrderDetailsId",
                table: "OrderDetailsSparePartIds",
                column: "OrderDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairHistoryNotificationIds_RepairHistoryId",
                table: "RepairHistoryNotificationIds",
                column: "RepairHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledMaintenanceNotificationIds_ScheduledMaintenanceId",
                table: "ScheduledMaintenanceNotificationIds",
                column: "ScheduledMaintenanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledMaintenanceTypes_ScheduledMaintenanceId",
                table: "ScheduledMaintenanceTypes",
                column: "ScheduledMaintenanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategoryServices_ServicePartnerServiceCategoryId_ServicePartnerId",
                table: "ServiceCategoryServices",
                columns: new[] { "ServicePartnerServiceCategoryId", "ServicePartnerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceHistoryTickets_OrderDetailsId",
                table: "ServiceHistoryTickets",
                column: "OrderDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceHistoryTickets_ServiceHistoryId",
                table: "ServiceHistoryTickets",
                column: "ServiceHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceLocations_ServicePartnerId",
                table: "ServiceLocations",
                column: "ServicePartnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServicePartnerMechanicProfileIds_ServicePartnerId",
                table: "ServicePartnerMechanicProfileIds",
                column: "ServicePartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePartnerPhotoIds_ServicePartnerId",
                table: "ServicePartnerPhotoIds",
                column: "ServicePartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePartnerReviewIds_ServicePartnerId",
                table: "ServicePartnerReviewIds",
                column: "ServicePartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePartnerServiceCategories_ServicePartnerId",
                table: "ServicePartnerServiceCategories",
                column: "ServicePartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileReviewIds_UserProfileId",
                table: "UserProfileReviewIds",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclePhotoIds_VehicleId",
                table: "VehiclePhotoIds",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreakTimes");

            migrationBuilder.DropTable(
                name: "ExpenseHistories");

            migrationBuilder.DropTable(
                name: "MechanicProfileReviewIds");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OrderDetailsServiceIds");

            migrationBuilder.DropTable(
                name: "OrderDetailsSparePartIds");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "RegisteredUsers");

            migrationBuilder.DropTable(
                name: "RepairHistoryNotificationIds");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ScheduledMaintenanceNotificationIds");

            migrationBuilder.DropTable(
                name: "ScheduledMaintenanceTypes");

            migrationBuilder.DropTable(
                name: "ServiceCategoryServices");

            migrationBuilder.DropTable(
                name: "ServiceHistoryTickets");

            migrationBuilder.DropTable(
                name: "ServiceLocations");

            migrationBuilder.DropTable(
                name: "ServicePartnerMechanicProfileIds");

            migrationBuilder.DropTable(
                name: "ServicePartnerPhotoIds");

            migrationBuilder.DropTable(
                name: "ServicePartnerReviewIds");

            migrationBuilder.DropTable(
                name: "SpareParts");

            migrationBuilder.DropTable(
                name: "SparePartsCategories");

            migrationBuilder.DropTable(
                name: "UserProfileReviewIds");

            migrationBuilder.DropTable(
                name: "VehiclePhotoIds");

            migrationBuilder.DropTable(
                name: "WorkSchedules");

            migrationBuilder.DropTable(
                name: "MechanicProfiles");

            migrationBuilder.DropTable(
                name: "RepairHistories");

            migrationBuilder.DropTable(
                name: "ScheduledMaintenances");

            migrationBuilder.DropTable(
                name: "ServicePartnerServiceCategories");

            migrationBuilder.DropTable(
                name: "OrderDetailsCollection");

            migrationBuilder.DropTable(
                name: "ServiceHistories");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "ServicePartners");
        }
    }
}

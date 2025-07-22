using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Travellin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AmenityCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileUploads",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    Path = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileUploads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuestTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertySpaceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertySpaceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amenities_AmenityCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AmenityCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User1Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    User2Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversations_AspNetUsers_User1Id",
                        column: x => x.User1Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Conversations_AspNetUsers_User2Id",
                        column: x => x.User2Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HostUpgradeRequests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FrontPhotoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BackPhotoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostUpgradeRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HostUpgradeRequests_AspNetUsers_ApprovedBy",
                        column: x => x.ApprovedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_HostUpgradeRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_HostUpgradeRequests_FileUploads_BackPhotoId",
                        column: x => x.BackPhotoId,
                        principalTable: "FileUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_HostUpgradeRequests_FileUploads_FrontPhotoId",
                        column: x => x.FrontPhotoId,
                        principalTable: "FileUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PropertySpaceItemTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PropertySpaceTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertySpaceItemTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertySpaceItemTypes_PropertySpaceTypes_PropertySpaceTypeId",
                        column: x => x.PropertySpaceTypeId,
                        principalTable: "PropertySpaceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TranslatedContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Messages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PhotoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProfiles_FileUploads_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "FileUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyTypeId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    SafteyInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseRules = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancellationPolicy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Properties_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "PropertyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    TotalFees = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Bookings_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteProperties",
                columns: table => new
                {
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteProperties", x => new { x.PropertyId, x.UserId });
                    table.ForeignKey(
                        name: "FK_FavoriteProperties_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FavoriteProperties_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PropertyAmenities",
                columns: table => new
                {
                    AmenityId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyAmenities", x => new { x.PropertyId, x.AmenityId });
                    table.ForeignKey(
                        name: "FK_PropertyAmenities_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PropertyAmenities_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PropertyAvailabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2(3)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2(3)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyAvailabilities_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PropertyFees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyFees_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PropertyGuests",
                columns: table => new
                {
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GuestTypeId = table.Column<int>(type: "int", nullable: false),
                    GuestCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyGuests", x => new { x.PropertyId, x.GuestTypeId });
                    table.ForeignKey(
                        name: "FK_PropertyGuests_GuestTypes_GuestTypeId",
                        column: x => x.GuestTypeId,
                        principalTable: "GuestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PropertyGuests_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PropertyPhotos",
                columns: table => new
                {
                    PhotoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TouchedAt = table.Column<DateTime>(type: "datetime2(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyPhotos", x => x.PhotoId);
                    table.ForeignKey(
                        name: "FK_PropertyPhotos_FileUploads_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "FileUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PropertyPhotos_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PropertySpaces",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PropertySpaceTypeId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsShared = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertySpaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertySpaces_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PropertySpaces_PropertySpaceTypes_PropertySpaceTypeId",
                        column: x => x.PropertySpaceTypeId,
                        principalTable: "PropertySpaceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Violations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportedPropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReportedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResolvedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReportedById1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportedPropertyId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReportedUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Violations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Violations_AspNetUsers_ReportedById1",
                        column: x => x.ReportedById1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Violations_AspNetUsers_ReportedUserId1",
                        column: x => x.ReportedUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Violations_Properties_ReportedPropertyId1",
                        column: x => x.ReportedPropertyId1,
                        principalTable: "Properties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookingGuests",
                columns: table => new
                {
                    BookingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GuestTypeId = table.Column<int>(type: "int", nullable: false),
                    GuestCount = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingGuests", x => new { x.BookingId, x.GuestTypeId });
                    table.ForeignKey(
                        name: "FK_BookingGuests_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BookingGuests_GuestTypes_GuestTypeId",
                        column: x => x.GuestTypeId,
                        principalTable: "GuestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    BookingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StripeSessionId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StripePaymentIntentId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "usd"),
                    Status = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "Pending"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    BookingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cleanliness = table.Column<decimal>(type: "decimal(2,1)", nullable: false),
                    Accuracy = table.Column<decimal>(type: "decimal(2,1)", nullable: false),
                    CheckIn = table.Column<decimal>(type: "decimal(2,1)", nullable: false),
                    Communication = table.Column<decimal>(type: "decimal(2,1)", nullable: false),
                    Location = table.Column<decimal>(type: "decimal(2,1)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(2,1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PropertySpaceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertySpaceItemTypeId = table.Column<int>(type: "int", nullable: true),
                    PropertySpaceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertySpaceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertySpaceItems_PropertySpaceItemTypes_PropertySpaceItemTypeId",
                        column: x => x.PropertySpaceItemTypeId,
                        principalTable: "PropertySpaceItemTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropertySpaceItems_PropertySpaces_PropertySpaceId",
                        column: x => x.PropertySpaceId,
                        principalTable: "PropertySpaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "AmenityCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Essentials" },
                    { 2, "Kitchen and dining" },
                    { 3, "Home safety" },
                    { 4, "Entertainment" },
                    { 5, "Outdoor" },
                    { 6, "Parking and facilities" },
                    { 7, "Heating and cooling" },
                    { 8, "Bedroom and laundry" },
                    { 9, "Bathroom" },
                    { 10, "Family" },
                    { 11, "Internet and office" },
                    { 12, "Location features" },
                    { 13, "Services" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "59ebef1f-d79b-4db0-9c5a-304836f14ff1", null, "Host", "HOST" },
                    { "9c75a5df-20a4-4ff1-85a5-bb52f9cf223f", null, "Guest", "GUEST" },
                    { "d35a86a5-72b3-4e7e-bb7f-5ef782b36f7c", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2dacdb51-fee9-4479-904c-cafe7dca22a6", 0, "2bc5ed7c-f23c-41b2-8f24-6cde1379cf70", "admin@email.com", true, false, null, "ADMIN@EMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEC7spMPg5RTE/+JwhaFMZ9D4qe125yj/pgHQRdpqvzZn/yUZ56sxPK6NYZ+WPproog==", null, false, "2O776OTQMPGHNUKGKGVD7EK56EWEHWJ4", false, "admin" },
                    { "3dacdb51-fee9-4479-904c-cafe7dca22a7", 0, "3bc5ed7c-f23c-41b2-8f24-6cde1379cf70", "host@email.com", true, false, null, "HOST@EMAIL.COM", "HOST", "AQAAAAIAAYagAAAAEC7spMPg5RTE/+JwhaFMZ9D4qe125yj/pgHQRdpqvzZn/yUZ56sxPK6NYZ+WPproog==", null, false, "HOSTSTAMP", false, "host" },
                    { "4dacdb51-fee9-4479-904c-cafe7dca22a8", 0, "4bc5ed7c-f23c-41b2-8f24-6cde1379cf70", "guest@email.com", true, false, null, "GUEST@EMAIL.COM", "GUEST", "AQAAAAIAAYagAAAAEC7spMPg5RTE/+JwhaFMZ9D4qe125yj/pgHQRdpqvzZn/yUZ56sxPK6NYZ+WPproog==", null, false, "GUESTSTAMP", false, "guest" }
                });

            migrationBuilder.InsertData(
                table: "FileUploads",
                columns: new[] { "Id", "Path" },
                values: new object[,]
                {
                    { "0184da01-3f04-431a-821b-863db48eee6b", "images/maadi1.avif" },
                    { "08f3b524-1ff0-4d1f-a4f9-a50c0d6ee717", "images/Dub1.avif" },
                    { "0f18b242-e627-45eb-a22d-516722b7c78c", "images/Vit1.avif" },
                    { "11010e1b-3c99-4d25-a176-9b826b19ec88", "images/portugal3.avif" },
                    { "1389e44f-240f-4eed-bde3-93623d7c41d1", "images/Dub2.avif" },
                    { "160b2604-8211-42b5-9f78-4360d5a71ee9", "images/canada2.avif" },
                    { "1cc7082f-8324-4888-b903-9d8ed2ffd144", "images/portugal2.avif" },
                    { "1d0aa7e5-30b6-42f6-aa21-11fed6d12c9a", "images/canada1.avif" },
                    { "1fb978f8-fb49-4f38-8acb-345be5c86bc7", "images/indon1.jpeg" },
                    { "26d418bb-0f90-4f3c-b339-7dd5c31b5e99", "images/california1.jpeg" },
                    { "2ac68b52-e7b6-4bb7-9f8e-49aa7f2b2b6c", "images/italy1.webp" },
                    { "2cf95d6d-63ae-4b97-8101-c6c5e8227b6d", "images/barcelona1.avif" },
                    { "2f50cb6f-8aeb-4428-8279-7c3a11d18232", "images/Turk2.avif" },
                    { "301f7e01-cc25-48ed-90aa-fafe16fce3b5", "images/Vit3.avif" },
                    { "303c12b0-baca-42d4-824e-d84b940d317a", "images/Nig3.avif" },
                    { "3588517b-0a71-4d29-ad8c-906a8e545d00", "images/Belg2.avif" },
                    { "3777d149-0028-4ea1-ba62-db41d33939f5", "images/Colm3.avif" },
                    { "3cb5e765-921f-4e0e-97be-b6d1e4c762cf", "images/Rom3.avif" },
                    { "4ae9e354-5eac-4f3a-a4b3-7c84c5b31d89", "images/guest.jpg" },
                    { "4b0f81f1-9bc0-45c6-988e-1a4fd270b3e0", "images/egy3.avif" },
                    { "4c376b94-d74f-4472-b1a5-4c3d51df56d8", "images/Arg2.jpg" },
                    { "4dfe3d56-2d34-4a6b-9cb5-f7a5a2dd8c28", "images/makkah2.avif" },
                    { "51d1e109-dccf-45fd-9f15-bbd3c0b7fcd5", "images/jordan3.avif" },
                    { "55a42f5d-4934-41df-8077-4ea9654c8d4f", "images/saf1.avif" },
                    { "5b742ed2-28d9-4e3b-8125-6e9c4587a0d3", "images/brazil3.avif" },
                    { "5c8fa3e9-2590-44d4-8e36-ee7f3c526b37", "images/Colm2.avif" },
                    { "5e2e82a1-4893-4a63-9375-d73f7a09d7c5", "images/barcelona3.avif" },
                    { "68b3f994-ed3d-461e-89b2-13ebe89d53b6", "images/saf3.avif" },
                    { "69c6c01e-65b3-4cf7-bbc7-2e94272b658a", "images/italy2.avif" },
                    { "6c54a231-b88f-409f-b5d5-170180930186", "images/makkah3.avif" },
                    { "6c79893d-f97f-4fc6-b0c3-4ebfcab3f85f", "images/france1.avif" },
                    { "6f572d60-4465-40a4-8b63-7bb2eb876cbd", "images/Colm1.avif" },
                    { "7a18064f-b6cb-4d58-a51b-0e8a74eac7a4", "images/makkah1.avif" },
                    { "7ae244c8-42a8-422c-9be6-b809c1b427f6", "images/soul2.avif" },
                    { "7d861c0c-011d-4b2a-8ce5-f5b1f0b81d01", "images/jordan1.avif" },
                    { "82da2e46-5e65-4ad8-97e9-ad10fdd63171", "images/Mroc2.avif" },
                    { "84d8e12a-4754-4825-b0fc-2b43981f6ba0", "images/vg1.avif" },
                    { "87668883-00e2-4d99-9dea-b612fb1f09fb", "images/maadi2.avif" },
                    { "89f65612-5023-489e-9604-2f01074abf0c", "images/california3.avif" },
                    { "90cd8c79-cc01-4edf-85b8-9931cd3fc772", "images/Belg1.avif" },
                    { "9201fad7-d63f-4dbf-84f1-adb25c451e9e", "images/Nig2.avif" },
                    { "95cde2b1-305e-4c13-9293-8c4c8f7c8b9f", "images/italy3.avif" },
                    { "98a76538-918f-4e60-9c01-b364e0e1891f", "images/france2.avif" },
                    { "98b7dcb6-7c53-4216-9f7a-259f40371fd4", "images/host.jpg" },
                    { "9b0d97e4-6dad-4e5b-893c-38aaff4a50e2", "images/Nig1.avif" },
                    { "9ea371c2-fefe-423f-953c-c744a33d5fb9", "images/Arg1.avif" },
                    { "9f90c24f-0a95-46a3-a2e2-a0688c460a23", "images/canada3.avif" },
                    { "a05afc7a-9127-4a33-839c-908e1f47a4ae", "images/tun3.avif" },
                    { "a2529147-026b-4b8b-a811-cb18989a8129", "images/Vit2.avif" },
                    { "a4c0d40d-e90e-4b14-8a2a-5ac0212be9b1", "images/california2.jpeg" },
                    { "a6d2fafb-6490-4f6f-a4c7-f42fdde98bf2", "images/soul1.avif" },
                    { "a73860df-b173-4d4d-b834-124f19d93a20e", "images/Rom2.avif" },
                    { "aac5407d-a994-4c3e-a1ff-b7646d79162a", "images/tun2webp.webp" },
                    { "ab39dc17-5108-425b-8350-1995323ba1a1", "images/soul3.avif" },
                    { "aca8279a-04bd-4277-8370-1338beb17581", "images/maadi3.avif" },
                    { "b01df5ef-3951-4e4c-80c5-00e10029a682", "images/saf2.avif" },
                    { "b21f8f4f-6d95-4f60-81b4-56d2ef017a08", "images/brazil1.avif" },
                    { "b455bb0a-69a3-4024-b5fa-5a49323e58fd", "images/egy1.avif" },
                    { "ba47797b-da79-47a0-8014-48e5422f0500", "images/indon2.jpeg" },
                    { "c3d1f440-7e0e-4f38-8b5d-34ea8d12e801", "images/admin.jpg" },
                    { "caf5622f-99a6-4927-a913-48d66437de5d", "images/vg3.avif" },
                    { "ce9e31d6-6553-4214-8b94-fb9c8f3065ed", "images/barcelona2.avif" },
                    { "da2afaf9-b1df-4daf-bb44-3d6a79be4a17", "images/portugal1.avif" },
                    { "da734851-2db8-4541-a788-b675b7560eec", "images/Belg3.avif" },
                    { "dc16e3d2-16ed-4ff5-b9c2-27a1e8b5ccbe", "images/egy2.avif" },
                    { "e019ead5-3b99-4f78-a84b-23b34ba27e26", "images/indon3.jpeg" },
                    { "e0f27e50-9e45-489b-90d6-f62211f67f12", "images/vg2.avif" },
                    { "e34a2808-38df-4e47-8c3e-d6e3f2712f11", "images/brazil2.avif" },
                    { "e3d6bbae-2087-4269-b6ca-784e3301cce0", "images/tun1.avif" },
                    { "e4a28523-f13c-431a-8af5-2ebd307f1a85", "images/Mroc3.avif" },
                    { "e56c967f-ee64-4e53-b0e6-1b1342baf2da", "images/Arg3.jpg" },
                    { "ec263c4f-fcc3-4d72-805e-d0b116e2cdd7", "images/Turk1.avif" },
                    { "f3885b77-0f9e-4ec3-9b3e-cbc194a07d7f", "images/france3.avif" },
                    { "f3db201e-fddd-4278-9beb-96863dde2f0f", "images/Morc1.webp" },
                    { "f4b528d3-1204-4ccc-af05-2a39346d7ace", "images/Rom1.avif" },
                    { "fbb7ade9-39b8-4b3b-abb5-b38fc1f70471", "images/Turk3.avif" },
                    { "fbc177de-bf4c-4b75-a1f6-884d05ce6c9f", "images/jordan2.avif" },
                    { "ff840d01-6eab-45fb-a911-674725a89003", "images/Dub3.avif" }
                });

            migrationBuilder.InsertData(
                table: "GuestTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Adults" },
                    { 2, "Childern" },
                    { 3, "Infants" },
                    { 4, "Pets" }
                });

            migrationBuilder.InsertData(
                table: "PropertySpaceTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bedroom" },
                    { 2, "Bathroom" },
                    { 3, "Kitchen" },
                    { 4, "Living Room" },
                    { 5, "Dining Area" },
                    { 6, "Workspace" },
                    { 7, "Laundry Area" },
                    { 8, "Private Entrance" },
                    { 9, "Balcony" },
                    { 10, "Patio" },
                    { 11, "Backyard" },
                    { 12, "Fire Pit" },
                    { 13, "Baby Room" },
                    { 14, "Children’s Play Area" },
                    { 15, "Closet" },
                    { 16, "Storage Space" }
                });

            migrationBuilder.InsertData(
                table: "PropertyTypes",
                columns: new[] { "Id", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "house", "House/Apartment" },
                    { 2, "bed-single", "Room" },
                    { 3, "hotel", "Hotel" },
                    { 4, "sun-moon", "Unique & Themed Stays" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "North America" },
                    { 2, "Europe" },
                    { 3, "Asia" },
                    { 4, "South America" },
                    { 5, "Africa & Oceania" }
                });

            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "Id", "CategoryId", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, 11, "wifi", "Wi-Fi" },
                    { 2, 2, "waves", "Pool" },
                    { 3, 7, "air-vent", "Air conditioning" },
                    { 4, 9, "bath", "Bathtub" },
                    { 5, 9, "wind", "Hair dryer" },
                    { 6, 9, "broom", "Cleaning products" },
                    { 7, 9, "droplets", "Shampoo" },
                    { 8, 9, "droplet", "Conditioner" },
                    { 9, 9, "soap", "Body soap" },
                    { 10, 9, "toilet", "Bidet" },
                    { 11, 9, "shower-head", "Outdoor shower" },
                    { 12, 9, "thermometer-sun", "Hot water" },
                    { 13, 9, "flask-round", "Shower gel" },
                    { 14, 8, "sparkles", "Free washer – In unit" },
                    { 15, 8, "shirt", "Hangers" },
                    { 16, 8, "bed-double", "Bed linens" },
                    { 17, 8, "pill", "Extra pillows and blankets" },
                    { 18, 8, "lamp", "Room-darkening shades" },
                    { 19, 8, "flame", "Iron" },
                    { 20, 8, "hanger", "Drying rack for clothing" },
                    { 21, 8, "bug", "Mosquito net" },
                    { 22, 8, "archive", "Clothing storage: closet and dresser" },
                    { 23, 4, "cable", "Ethernet connection" },
                    { 24, 4, "tv", "42 inch HDTV with Netflix" },
                    { 25, 4, "volume-2", "Sound system with aux" },
                    { 26, 4, "gamepad-2", "Game console" },
                    { 27, 4, "tennis", "Ping pong table" },
                    { 28, 4, "dice-5", "Pool table" },
                    { 29, 4, "book-open", "Books and reading material" },
                    { 30, 4, "film", "Movie theater" },
                    { 31, 10, "baby", "Crib" },
                    { 32, 10, "blocks", "Children’s books and toys" },
                    { 33, 10, "chair", "High chair" },
                    { 34, 10, "baby", "Baby bath" },
                    { 35, 10, "utensils-crossed", "Children’s dinnerware" },
                    { 36, 10, "dice-3", "Board games" },
                    { 37, 10, "door-closed", "Baby safety gates" },
                    { 38, 10, "user", "Babysitter recommendations" },
                    { 39, 10, "puzzle", "Children's playroom" },
                    { 40, 7, "flame", "Indoor fireplace: wood-burning" },
                    { 41, 7, "fan", "Ceiling fan" },
                    { 42, 7, "fan", "Portable fans" },
                    { 43, 7, "thermometer", "Heating" },
                    { 44, 3, "first-aid-kit", "First aid kit" },
                    { 45, 3, "laptop", "Dedicated workspace" },
                    { 46, 2, "chef-hat", "Kitchen" },
                    { 47, 2, "fridge", "Refrigerator" },
                    { 48, 2, "utensils-crossed", "Cooking basics" },
                    { 49, 2, "plate", "Dishes and silverware" },
                    { 50, 2, "snowflake", "Freezer" },
                    { 51, 2, "droplet", "Dishwasher" },
                    { 52, 2, "flame", "Stove" },
                    { 53, 2, "microwave", "Oven" },
                    { 54, 2, "kettle", "Hot water kettle" },
                    { 55, 2, "coffee", "Coffee maker" },
                    { 56, 2, "sandwich", "Toaster" },
                    { 57, 2, "sheet", "Baking sheet" },
                    { 58, 2, "blender", "Blender" },
                    { 59, 2, "knife", "Barbecue utensils" },
                    { 60, 2, "coffee", "Coffee" },
                    { 61, 12, "door-open", "Private entrance" },
                    { 62, 12, "shirt", "Laundromat nearby" },
                    { 63, 12, "mountain", "Balacony" },
                    { 64, 12, "fire-extinguisher", "Fire Pit" },
                    { 65, 12, "sofa", "Outdoor furniture" },
                    { 66, 13, "paw-print", "Pets Allowed" },
                    { 67, 6, "car", "Free street parking" },
                    { 68, 6, "car", "Free street On premises" },
                    { 69, 13, "calendar", "Long term stays allowed" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "d35a86a5-72b3-4e7e-bb7f-5ef782b36f7c", "2dacdb51-fee9-4479-904c-cafe7dca22a6" },
                    { "59ebef1f-d79b-4db0-9c5a-304836f14ff1", "3dacdb51-fee9-4479-904c-cafe7dca22a7" },
                    { "9c75a5df-20a4-4ff1-85a5-bb52f9cf223f", "4dacdb51-fee9-4479-904c-cafe7dca22a8" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name", "RegionId" },
                values: new object[,]
                {
                    { 1, "Afghanistan", 3 },
                    { 2, "Åland Islands", 2 },
                    { 3, "Albania", 2 },
                    { 4, "Algeria", 5 },
                    { 5, "American Samoa", 5 },
                    { 6, "Andorra", 2 },
                    { 7, "Angola", 5 },
                    { 8, "Anguilla", 5 },
                    { 9, "Antarctica", 5 },
                    { 10, "Antigua and Barbuda", 5 },
                    { 11, "Argentina", 4 },
                    { 12, "Armenia", 3 },
                    { 13, "Aruba", 5 },
                    { 14, "Australia", 5 },
                    { 15, "Austria", 2 },
                    { 16, "Azerbaijan", 3 },
                    { 17, "Bahamas", 4 },
                    { 18, "Bahrain", 3 },
                    { 19, "Bangladesh", 3 },
                    { 20, "Barbados", 4 },
                    { 21, "Belarus", 2 },
                    { 22, "Belgium", 2 },
                    { 23, "Belize", 4 },
                    { 24, "Benin", 5 },
                    { 25, "Bermuda", 4 },
                    { 26, "Bhutan", 3 },
                    { 27, "Bolivia", 4 },
                    { 28, "Bosnia and Herzegovina", 2 },
                    { 29, "Botswana", 5 },
                    { 30, "Bouvet Island", 5 },
                    { 31, "Brazil", 4 },
                    { 32, "British Indian Ocean Territory", 5 },
                    { 33, "Brunei Darussalam", 3 },
                    { 34, "Bulgaria", 2 },
                    { 35, "Burkina Faso", 5 },
                    { 36, "Burundi", 5 },
                    { 37, "Cambodia", 3 },
                    { 38, "Cameroon", 5 },
                    { 39, "Canada", 1 },
                    { 40, "Cape Verde", 5 },
                    { 41, "Cayman Islands", 4 },
                    { 42, "Central African Republic", 5 },
                    { 43, "Chad", 5 },
                    { 44, "Chile", 4 },
                    { 45, "China", 3 },
                    { 46, "Christmas Island", 5 },
                    { 47, "Cocos (Keeling) Islands", 5 },
                    { 48, "Colombia", 4 },
                    { 49, "Comoros", 5 },
                    { 50, "Congo", 5 },
                    { 51, "Congo, The Democratic Republic of the", 5 },
                    { 52, "Cook Islands", 5 },
                    { 53, "Costa Rica", 4 },
                    { 54, "Cote D'Ivoire", 5 },
                    { 55, "Croatia", 2 },
                    { 56, "Cuba", 4 },
                    { 57, "Cyprus", 2 },
                    { 58, "Czech Republic", 2 },
                    { 59, "Denmark", 2 },
                    { 60, "Djibouti", 5 },
                    { 61, "Dominica", 5 },
                    { 62, "Dominican Republic", 4 },
                    { 63, "Ecuador", 4 },
                    { 64, "Egypt", 5 },
                    { 65, "El Salvador", 4 },
                    { 66, "Equatorial Guinea", 5 },
                    { 67, "Eritrea", 5 },
                    { 68, "Estonia", 2 },
                    { 69, "Ethiopia", 5 },
                    { 70, "Falkland Islands (Malvinas)", 4 },
                    { 71, "Faroe Islands", 2 },
                    { 72, "Fiji", 5 },
                    { 73, "Finland", 2 },
                    { 74, "France", 2 },
                    { 75, "French Guiana", 4 },
                    { 76, "French Polynesia", 5 },
                    { 77, "French Southern Territories", 5 },
                    { 78, "Gabon", 5 },
                    { 79, "Gambia", 5 },
                    { 80, "Georgia", 3 },
                    { 81, "Germany", 2 },
                    { 82, "Ghana", 5 },
                    { 83, "Gibraltar", 2 },
                    { 84, "Greece", 2 },
                    { 85, "Greenland", 1 },
                    { 86, "Grenada", 5 },
                    { 87, "Guadeloupe", 5 },
                    { 88, "Guam", 5 },
                    { 89, "Guatemala", 4 },
                    { 90, "Guernsey", 2 },
                    { 91, "Guinea", 5 },
                    { 92, "Guinea-Bissau", 5 },
                    { 93, "Guyana", 4 },
                    { 94, "Haiti", 4 },
                    { 95, "Heard Island and McDonald Islands", 5 },
                    { 96, "Honduras", 4 },
                    { 97, "Hong Kong", 3 },
                    { 98, "Hungary", 2 },
                    { 99, "Iceland", 2 },
                    { 100, "India", 3 },
                    { 101, "Indonesia", 3 },
                    { 102, "Iran", 3 },
                    { 103, "Iraq", 3 },
                    { 104, "Ireland", 2 },
                    { 105, "Israel", 3 },
                    { 106, "Italy", 2 },
                    { 107, "Jamaica", 4 },
                    { 108, "Japan", 3 },
                    { 109, "Jersey", 2 },
                    { 110, "Jordan", 3 },
                    { 111, "Kazakhstan", 3 },
                    { 112, "Kenya", 5 },
                    { 113, "Kiribati", 5 },
                    { 114, "Korea, Democratic People's Republic of", 3 },
                    { 115, "Korea, Republic of", 3 },
                    { 116, "Kuwait", 3 },
                    { 117, "Kyrgyzstan", 3 },
                    { 118, "Lao People's Democratic Republic", 3 },
                    { 119, "Latvia", 2 },
                    { 120, "Lebanon", 3 },
                    { 121, "Lesotho", 5 },
                    { 122, "Liberia", 5 },
                    { 123, "Libya", 5 },
                    { 124, "Liechtenstein", 2 },
                    { 125, "Lithuania", 2 },
                    { 126, "Luxembourg", 2 },
                    { 127, "Macao", 3 },
                    { 128, "Madagascar", 5 },
                    { 129, "Malawi", 5 },
                    { 130, "Malaysia", 3 },
                    { 131, "Maldives", 3 },
                    { 132, "Mali", 5 },
                    { 133, "Malta", 2 },
                    { 134, "Marshall Islands", 5 },
                    { 135, "Martinique", 5 },
                    { 136, "Mauritania", 5 },
                    { 137, "Mauritius", 5 },
                    { 138, "Mayotte", 5 },
                    { 139, "Mexico", 4 },
                    { 140, "Micronesia (Federated States of)", 5 },
                    { 141, "Moldova (Republic of)", 2 },
                    { 142, "Monaco", 2 },
                    { 143, "Mongolia", 3 },
                    { 144, "Montenegro", 2 },
                    { 145, "Montserrat", 5 },
                    { 146, "Morocco", 5 },
                    { 147, "Mozambique", 5 },
                    { 148, "Myanmar", 3 },
                    { 149, "Namibia", 5 },
                    { 150, "Nauru", 5 },
                    { 151, "Nepal", 3 },
                    { 152, "Netherlands", 2 },
                    { 153, "New Caledonia", 5 },
                    { 154, "New Zealand", 5 },
                    { 155, "Nicaragua", 4 },
                    { 156, "Niger", 5 },
                    { 157, "Nigeria", 5 },
                    { 158, "Niue", 5 },
                    { 159, "Norfolk Island", 5 },
                    { 160, "North Macedonia", 2 },
                    { 161, "Northern Mariana Islands", 5 },
                    { 162, "Norway", 2 },
                    { 163, "Oman", 3 },
                    { 164, "Pakistan", 3 },
                    { 165, "Palau", 5 },
                    { 166, "Palestine", 3 },
                    { 167, "Panama", 4 },
                    { 168, "Papua New Guinea", 5 },
                    { 169, "Paraguay", 4 },
                    { 170, "Peru", 4 },
                    { 171, "Philippines", 3 },
                    { 172, "Pitcairn", 5 },
                    { 173, "Poland", 2 },
                    { 174, "Portugal", 2 },
                    { 175, "Puerto Rico", 4 },
                    { 176, "Qatar", 3 },
                    { 177, "Romania", 2 },
                    { 178, "Russian Federation", 3 },
                    { 179, "Rwanda", 5 },
                    { 180, "Réunion", 5 },
                    { 181, "Saint Barthélemy", 5 },
                    { 182, "Saint Helena, Ascension and Tristan da Cunha", 5 },
                    { 183, "Saint Kitts and Nevis", 4 },
                    { 184, "Saint Lucia", 5 },
                    { 185, "Saint Martin", 5 },
                    { 186, "Saint Pierre and Miquelon", 5 },
                    { 187, "Saint Vincent and the Grenadines", 5 },
                    { 188, "Samoa", 5 },
                    { 189, "San Marino", 2 },
                    { 190, "Sao Tome and Principe", 5 },
                    { 191, "Saudi Arabia", 3 },
                    { 192, "Senegal", 5 },
                    { 193, "Serbia", 2 },
                    { 194, "Seychelles", 5 },
                    { 195, "Sierra Leone", 5 },
                    { 196, "Singapore", 3 },
                    { 197, "Sint Maarten", 5 },
                    { 198, "Slovakia", 2 },
                    { 199, "Slovenia", 2 },
                    { 200, "Solomon Islands", 5 },
                    { 201, "Somalia", 5 },
                    { 202, "South Africa", 5 },
                    { 203, "South Georgia and the South Sandwich Islands", 5 },
                    { 204, "South Sudan", 5 },
                    { 205, "Spain", 2 },
                    { 206, "Sri Lanka", 3 },
                    { 207, "Sudan", 5 },
                    { 208, "Suriname", 4 },
                    { 209, "Svalbard", 2 },
                    { 210, "Swaziland", 5 },
                    { 211, "Sweden", 2 },
                    { 212, "Switzerland", 2 },
                    { 213, "Syrian Arab Republic", 3 },
                    { 214, "Taiwan", 3 },
                    { 215, "Tajikistan", 3 },
                    { 216, "Tanzania (United Republic of)", 5 },
                    { 217, "Thailand", 3 },
                    { 218, "Timor-Leste", 3 },
                    { 219, "Togo", 5 },
                    { 220, "Tokelau", 5 },
                    { 221, "Tonga", 5 },
                    { 222, "Trinidad and Tobago", 4 },
                    { 223, "Tunisia", 5 },
                    { 224, "Turkey", 3 },
                    { 225, "Turkmenistan", 3 },
                    { 226, "Tuvalu", 5 },
                    { 227, "Uganda", 5 },
                    { 228, "Ukraine", 2 },
                    { 229, "United Arab Emirates", 3 },
                    { 230, "United Kingdom of Great Britain and Northern Ireland", 2 },
                    { 231, "United States of America", 1 },
                    { 232, "Uruguay", 4 },
                    { 233, "Uzbekistan", 3 },
                    { 234, "Vanuatu", 5 },
                    { 235, "Venezuela (Bolivarian Republic of)", 4 },
                    { 236, "Viet Nam", 3 },
                    { 237, "Western Sahara", 5 },
                    { 238, "Yemen", 3 },
                    { 239, "Zambia", 5 },
                    { 240, "Zimbabwe", 5 },
                    { 241, "Antarctica", 5 },
                    { 242, "Ascension Island", 5 },
                    { 243, "French Guiana", 5 }
                });

            migrationBuilder.InsertData(
                table: "PropertySpaceItemTypes",
                columns: new[] { "Id", "Name", "PropertySpaceTypeId" },
                values: new object[,]
                {
                    { 1, "Queen Bed", 1 },
                    { 2, "Nightstand", 1 },
                    { 3, "Wardrobe", 1 },
                    { 4, "Shower", 2 },
                    { 5, "Bathtub", 2 },
                    { 6, "Toilet", 2 },
                    { 7, "Stove", 3 },
                    { 8, "Refrigerator", 3 },
                    { 9, "Microwave", 3 },
                    { 10, "Sofa", 4 },
                    { 11, "Coffee Table", 4 },
                    { 12, "TV Stand", 4 },
                    { 13, "Dining Table", 5 },
                    { 14, "Dining Chairs", 5 },
                    { 15, "Sideboard", 5 },
                    { 16, "Desk", 6 },
                    { 17, "Office Chair", 6 },
                    { 18, "Desk Lamp", 6 },
                    { 19, "Washer", 7 },
                    { 20, "Dryer", 7 },
                    { 21, "Ironing Board", 7 },
                    { 22, "Doormat", 8 },
                    { 23, "Shoe Rack", 8 },
                    { 24, "Umbrella Stand", 8 },
                    { 25, "Outdoor Chair", 9 },
                    { 26, "Small Table", 9 },
                    { 27, "Planter Box", 9 },
                    { 28, "Patio Furniture", 10 },
                    { 29, "Grill", 10 },
                    { 30, "Shade Umbrella", 10 },
                    { 31, "Picnic Table", 11 },
                    { 32, "Garden Bench", 11 },
                    { 33, "Hammock", 11 },
                    { 34, "Fire Pit Ring", 12 },
                    { 35, "Seating", 12 },
                    { 36, "Log Holder", 12 },
                    { 37, "Crib", 13 },
                    { 38, "Changing Table", 13 },
                    { 39, "Baby Monitor", 13 },
                    { 40, "Toy Box", 14 },
                    { 41, "Play Rug", 14 },
                    { 42, "Slide", 14 },
                    { 43, "Hangers", 15 },
                    { 44, "Shelving Unit", 15 },
                    { 45, "Shoe Organizer", 15 },
                    { 46, "Storage Boxes", 16 },
                    { 47, "Shelves", 16 },
                    { 48, "Plastic Bins", 16 }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 64, "Nazlet El-Semman, Giza Governorate" },
                    { 2, 106, "Milan, Lombardia" },
                    { 3, 191, "Makkah, Makkah Province" },
                    { 4, 231, "Yucca Valley, California" },
                    { 5, 31, "Salvador, Bahia" },
                    { 6, 205, "Barcelona, Catalunya" },
                    { 7, 110, "Wadi Rum Village, Aqaba Governorate" },
                    { 8, 74, "Tanneron, Provence-Alpes-Côte d'Azur" },
                    { 9, 174, "Windmill in Ponta Delgada, Portugal" },
                    { 10, 114, "Soul ,South Korea  " },
                    { 11, 202, "Cape Town, Western Cape, South Africa" },
                    { 12, 64, "Maadi , Egypt" },
                    { 13, 39, "Courtenay, Canada" },
                    { 14, 231, "Virginia, United States" },
                    { 15, 223, " Gammarth, Tunisia" },
                    { 16, 22, "Jodoigne, Belgium" },
                    { 17, 11, "Buenos Aires, Argentina" },
                    { 18, 157, "Lekki, Nigeria" },
                    { 19, 236, "Lâm Thượng, Vietnam" },
                    { 20, 229, "Dubai, United Arab Emirates" },
                    { 21, 146, "Imlil, Morocco" },
                    { 22, 48, " Sasaima,  Colombia" },
                    { 23, 101, "Kabupaten Gianyar, Indonesia" },
                    { 24, 106, "Rome, Italy" },
                    { 25, 224, "Bodrum, Turkey" }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "UserId", "Bio", "BirthDate", "CountryId", "FirstName", "LastName", "PhotoId" },
                values: new object[,]
                {
                    { "2dacdb51-fee9-4479-904c-cafe7dca22a6", "As the system administrator, I ensure that our platform runs smoothly, securely, and efficiently. From managing users and listings to maintaining system integrity, I'm here to support both guests and hosts for a seamless experience.", new DateOnly(1995, 5, 2), 64, "Marcus", "Dou", "c3d1f440-7e0e-4f38-8b5d-34ea8d12e801" },
                    { "3dacdb51-fee9-4479-904c-cafe7dca22a7", "Hi, I’m Pavel! I’ve been hosting guests from around the world for over 3 years. I love sharing my cozy home and local tips to help you experience the best of the city. Your comfort and privacy are my top priorities—feel free to reach out with any questions before or during your stay!", new DateOnly(1999, 12, 2), 64, "Pavel", "Elmo", "98b7dcb6-7c53-4216-9f7a-259f40371fd4" },
                    { "4dacdb51-fee9-4479-904c-cafe7dca22a8", "Hi, I’m Lucas! I enjoy exploring new cities, meeting new people, and experiencing different cultures. I’m a respectful guest who values comfort and cleanliness. Looking forward to staying in your wonderful property and making the most of my travels!", new DateOnly(2001, 2, 2), 64, "lucas", "Martin", "4ae9e354-5eac-4f3a-a4b3-7c84c5b31d89" }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "CancellationPolicy", "Description", "HouseRules", "IsActive", "Latitude", "LocationId", "Longitude", "OwnerId", "PricePerNight", "PropertyTypeId", "SafteyInfo", "Title" },
                values: new object[,]
                {
                    { "06dbae08-bc6b-4ca6-9162-3213784b9971", "Free cancellation before May 5. Cancel before check-in on May 9 for a full refund.", "Xoi Farmstay is located in a green valley of Lam Thuong in the North of Vietnam, about 250km from Hanoi and near to Hagiang and Sapa.This is a place for those who love nature, watching rice fields, exotic mountains, spring and waterfall, authentic local culture, good food, especially non touristy", "Check-in brfore 1:00 Am , Checkout before 11:00 AM , 1 guests maximum", true, 21.05m, 19, 105.4333m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 100m, 4, "carbon monoxide alarm  ,No Smoke alarm", "TXoi Farmstay- Homefarm in the valley of Lam Thuong" },
                    { "0bb50f31-e322-4b76-97dd-6a7fcf585d33", "Free cancellation before May 2, Cancel before check-in on May 3 for a partial refund.", "With panoramic water views, Delta Hotels by Marriott Virginia Beach Waterfront is an oasis on the shores of the breathtaking Chesapeake Bay.Thrill your palate with fresh oysters, fish, and coastal cuisine at our distinctive hotel restaurant, featuring inspiring water views.", "Check-in: 4:00 PM - 12:00 AM , Checkout before 11:00 AM ,4 guests maximum", true, 37.5407m, 14, 77.436m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 90m, 3, "Carbon monoxide alarm, Smoke alarm", "Escape To Our Beachfront Oasis | Private Beach" },
                    { "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08", "Free cancellation before May 26. Cancel before check-in on May 14 for a partial refund.", "Comfortable room, queen bed, bathroom in suite, with air conditioning. Excelent location, among Palermo and Recoleta neighborhoods, one block away from Santa Fe av and 2 blocks away from subway line D.", "Check-in brfore 4:00 Am , Checkout before 9:00 AM , 2 guests maximum", true, 34.6037m, 17, 58.3816m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 190m, 2, "No carbon monoxide alarm  ,No Smoke alarm", "Palermo/Recoleta. Stylish room w/ensuite-bath & AC" },
                    { "294e2751-203b-4beb-b21e-0bb96f082d7c", "Free cancellation before May 3. Cancel before check-in on May 14 for a full refund.", "Charming industrial character and premium homely comfort in the most desirable location. A leisurely stroll away from the shopping, dining & nightlife of Admiralty Way, Lekki Phase 1.Relax in the swimming pool or enjoy movies on satellite, Netflix or Amazon. Superfast optic-fibre broadband wi-fi. Uninterrupted 24/7 generator power back-up.", "Check-in brfore 2:00 Am , Checkout before 9:00 AM , 3 guests maximum", true, 6.4367m, 18, 3.5244m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 200m, 1, "carbon monoxide alarm  , Smoke alarm", "The Foundry. Luxury 2BR w/pool" },
                    { "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1", "Free cancellation before May 19. Cancel before check-in on May 24 for a partial refund.", "This is a guitar-shaped country house located in Icheon, a ceramic art village. It is a private house with a spacious terrace on the 3rd floor of the Sera Guitar Culture Center, famous for its unique appearance in the Icheon Ceramic Art Village, which blends in very well with nature.", "Check-in: 3:00 PM - 12:00 AM  , Checkout before 11:00  AM , 2 guests maximum", true, 37.3154m, 10, 127.4052m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 180m, 1, "Carbon monoxide alarm not reported , Smoke alarm , Must climb stairs", "Emotional healing accommodation in Icheon-si, near Seoul" },
                    { "2e3ed231-a2a6-4961-a1ba-f232d56c6f35", "Free cancellation before Apr 29. Cancel before check-in on May 1 for a partial refund.", "You will feel special from the beginning to the end of your holiday at Inone Mucho Selection Hotel, located on the seafront with a private beach in one of the clearest bays of Asarlik.Our facility which is located 5 minutes drive away from Bodrum center and 5 minutes from Gumbet bar street by walk. You can have a pleasant time while sipping your cocktail at our Iconic Beach restaurant, accompanied by various events and DJ performances.", "Check-in brfore 1:00 PM , Checkout before 10:00 PM , 2 guests maximum", true, 37.0383m, 25, 27.4292m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 200m, 2, "carbon monoxide alarm  ,  Smoke alarm", "Inone Mucho Selection Hotel Deluxe Room B&B" },
                    { "3c0e361a-51df-4e03-b8d0-2d7601aa60f6", "Free cancellation before Jun 18. Cancel before check-in on Jun 23 for a partial refund.", "Maadi is an uptown , green suburb with villas and gardens. My building is a five storey building . It is in a quiet area but a few minutes-walk away from Rd 9 where there are shops, cafes and restaurants. Everything you need is right here yet in 15 mins u can be in center of town.", "Flexible check-in , 2 guests maximum , No pets", true, 29.9617m, 12, 31.2667m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 100m, 2, "No carbon monoxide alarm , No smoke alarm ,Nearby lake, river, other body of water", "sunny, spacious, clean room in maadi, cairo.." },
                    { "3e7f99ab-228a-4d90-91c4-6adf8c12e048", "Free cancellation before May 17 , Cancel before check-in on May 18 for a partial refund.", "Relax with this listing Small 2-room 7-bed apartment near Alharam Al Makkah with a maximum of 10 to 12 minutes' walk away The ears and prayer are also heard inside the rooms and the window appears from the window of the Haram Al-Sharif .We offer a Surface kitchen with tea and coffee supplies, a mini fridge, a microwave, a water kettle and more A washing machine is available and we provide toiletries from towels, shampoo, lotion, soap, and more We provide a wheelchair ,wi-fi .This place is in a high tower where the apartment is located on the 17th floor Wish you a unique and pleasant stay", "Check-in after 3:00 PM , Checkout before 12:00 PM , 7 guests maximum", true, 21.4266m, 3, 39.8256m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 90m, 1, "Carbon monoxide alarm ,Smoke alarm installed", "Rent an apartment near Alhar Mecca" },
                    { "4b04a76a-1608-4a8f-b09c-8d9043b83e16", "Free cancellation for 48 hours , Cancel before Jan 13 for a partial refund.", "Built in the 19th century, with a 360 degrees view over the sea and surroundings on the top floor.It features a Bedroom, a very well-decorated living room with kitchenette, and a WC.Free WiFi, air conditioning, Led TV and DVD player.Private parking inside the premises, providing extra security.Perfect for an unforgettable honeymoon experience.", "Check-in: 3:00 PM - 5:00 PM ,Checkout before 10:00 AM ,2 guests maximum", true, 37.7428m, 9, 25.6806m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 200m, 4, "Climbing or play structure , Carbon monoxide alarmSmoke alarm", "Moinho das Feteiras | The Mill House" },
                    { "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2", "Free cancellation before Oct 22 , Cancel before check-in on Oct 23 for a partial refund.", "Romantic Loft with mezzanine and large balcony in front of the sea, double bed and 1 single bed, tv, wi-fi, fan, cabinet modern decoration, 180 degree terrace to the sea, equipped kitchen, bathroom, total comfort and privacy, fourth floor without elevator, 5 minutes from the carnival circuit, Noble Quarter of the city. Between the Surf and Paciencia beaches. Total security. The most beautiful sunset in Salvador", "3 guests maximum , Pets allowed", true, -12.9711m, 5, -38.5108m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 130m, 1, "Carbon monoxide alarm not reported , Smoke alarm not reported , Exterior security cameras on property", "(4) charming oceanfront loft!" },
                    { "52a8df7d-c0b2-4ee3-8369-9daed4885f9f", "Free cancellation before Apr 26. Cancel before check-in on May 1 for a partial refund.", "Chill in a quite and fresh area only 3 min drive to Ubud center.Our villa located in the middle of rice field , offered you great experience.Friendly owner will assist you 24 hours by call to make sure you can enjoy the stay .Stay for 3 nights and you will get Free Traditional Balinese massage for 1 person for 60 min to complete the lazy days", "Check-in brfore 3:00 PM , Checkout before 12:00 PM , 3 guests maximum", true, -8.5441m, 23, 115.3255m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 110m, 1, "carbon monoxide alarm  , No Smoke alarm , Nearby lake, river, other body of water", "Quite Get Away near by theCenter" },
                    { "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", "Free cancellation before May 17 , Cancel before check-in on May 18 for a partial refund.", "Updated pool and spa! Sitting on 100 acres, Hawkeye House, featured on the cover of the May 2019 issue of Dwell Magazine, is an off grid Geodesic Dome. It has a 40 foot pool and hot tub that you will have to see to believe. This unique and modern home has been fully remodeled with an attention to both comfort and detail. Amazing hikes and privacy are abundant here. Most people never want to leave the property", "Check-in after 3:00 PM , Checkout before 12:00 PM , 7 guests maximum", true, 34.114174m, 4, -116.432236m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 110m, 2, "Carbon monoxide alarm ,Smoke alarm installed", "Hawkeye Dome - New Pool and Spa" },
                    { "763e6c5f-1ad1-4071-b0e6-55e924624198", "Free cancellation before May 5. Cancel before check-in on May 9 for a full refund.", "Dar Ouassaggou's owner, Houssine, is a fluent English speaker and looks forward to welcoming you to his friendly guesthouse retreat in the Atlas Mountains, A Warm Welcome Awaits you at Dar Ouassaggou.It is a small comfortable guest house with 13 en suite rooms and balcony .", "Check-in brfore 11:00 Am , Checkout before 12:00 AM , 3 guests maximum", true, 31.1333m, 21, 7.9167m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 220m, 2, "No carbon monoxide alarm  , Smoke alarm", "Atlas Mountains Riad Oussagou" },
                    { "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", "Free cancellation before May 17 , Cancel before check-in on May 18 for a partial refund.", "Elegant apartment inside the famous castle in Nolo, a royal choice right in the center of Milan A few steps away is the metro (M1 red for the Duomo 10 min), 10 minutes' walk for the central station. The apartment is well connected by trains, trams and buses The area is well supplied with restaurants, supermarkets, bars, clubs, etc. Complete comfort:82 Smart TV, Netflix, prime, wifi, dishwasher, kitchen, coffee machine The stay is included with a complete reception service", "Check-in: 3:00 PM - 11:00PM ,Checkout before 11:00 AM ,4 guests maximum", true, 45.46427m, 2, 9.18951m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 250m, 3, "Carbon monoxide alarm ,Smoke alarm installed", "Milano Duomo center 10 min Flat inside a castle" },
                    { "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", "Free cancellation before Jun 3. Cancel before check-in on Jun 4 for a partial refund", "Set in an architectural prize-winning building, this modern Barcelona apartment beauty has impressive detail throughout. Ceiling-to-floor sloped windows, wood floor, and other soft designer textures accentuate this spectacular space. It is cozy and welcoming but with a very hip, urban edge.Design enthusiasts and those looking for that modern Barcelona feel will love the apartment. However, high-comfort and proximity to the Sagrada Familia suits all tastes.", "Check-in: 3:00 PM - 5:00 PM ,Checkout before 10:00 AM ,2 guests maximum", true, 41.3888m, 6, 2.159m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 310m, 1, "No carbon monoxide alarm , No smoke alarm , Heights without rails or protectio", "Sunny and cozy Apartment Sagrada Familia" },
                    { "a555515a-ff8a-4741-b0a4-db9be729198e", "Free cancellation before May 4. Cancel before check-in on May 5 for a partial refund.", "Discover this luxury apartment in Gammarth, in the tourist area, with sea views and direct access to a private beach reserved for residents. The master suite includes a private bathroom, and a second bathroom is available", "Check-in after 3:00 PM,4 guests maximum,Pets allowed", true, 36.9475m, 15, 10.3036m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 20m, 4, "Carbon monoxide alarm not reported , Smoke alarm not reported", "Sea View S2: Waterfront, Private Beach" },
                    { "c10d2d46-869a-46bc-a46d-90bdd958c252", "Free cancellation before May 9. Cancel before check-in on May 14 for a partial refund.", "Warm and cosy cottage decorated with antique furniture, with a lovely garden. Perfect if you're looking for a relaxing stay in beautiful countryside. The bedroom windows have blackout blinds and the beds are very comfortable.", "Check-in: (4:00 PM - 10:00 PM) , Checkout before 11:00 AM , 4 guests maximum", true, 50.7236m, 16, 4.8694m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 230m, 1, "No carbon monoxide alarm , Nearby lake- river- other body of water , Smoke alarm", "Cosy English cottage with beautiful garden" },
                    { "c150e428-1c9a-43a2-be07-f4366875f1ce", "Free cancellation before Apr 29. Cancel before check-in on May 1 for a partial refund.", "Elegant and spacious apartment on the 4th floor, designed and realized for 6 people.Totally renovated in February 2025.,Composed of 2 double bedrooms, 1 single bedroom and a sofa bed in the dining room.,2 bathrooms of which one inside the double room.It is possible to access the terrace from each room.", "Check-in brfore 1:00 PM , Checkout before 10:00 PM , 2 guests maximum", true, 41.9028m, 24, 12.4964m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 90m, 3, "carbon monoxide alarm  ,  Smoke alarm", "[*Bright new Metro C penthouse*]." },
                    { "c5c0d4db-b048-4ee4-8835-344900fd35b2", "Add your trip dates to get the cancellation details for this stay.", "Charming small cottage situated on the edge of wetlands with beautiful views. Private gazebo with covered firepit and a dock over looking the large pond. Located on our 5 acre free range egg farm in Merville, BC. The pond is home to a family of beavers, bald eagles, blue heron and various birds. Private walking trail off the cottage and access to the One Spot Trail at the end of our private drive.", "Check-in after 3:00 PM,Checkout before 11:00 AM,2 guests maximum", true, 49.6876m, 13, 124.9936m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 400m, 4, "Exterior security cameras on property ,Carbon monoxide alarm , Smoke alarm", "Heather Cottage - Beautiful Wetland Views" },
                    { "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", "Free cancellation before May 28 , Cancel before check-in on Jun 2 for a partial refund.", "Enjoy your stay with Panoramic View of the giza pyramids and sphinx .Yes! view and pictures are all 100% real. (Be sure to check out our other listings too) Indulge in a stunning view of all the Giza Pyramids from anywhere within this contemporary oriental studio or while relaxing in the Jacuzzi. It is also a 10 min walk from the Pyramids entrance gate. To make the most of your trip, make sure to check out our experiences!We're committed to providing our guests the magical hospitality", "Check-in after 2:00 PM , Checkout before 11:00 AM , 2 guests maximum", true, 29.98333m, 1, 31.13333m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 100m, 1, "No Carbon monoxide alarm , No Smoke alarm ", "Entire rental unit in Nazlet El-Semman, Egypt" },
                    { "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", "Free cancellation before Jun 3. Cancel before check-in on Jun 4 for a partial refund", "Interior designer's own guesthouse, this unique place has a style all its own. Escape the ordinary and immerse yourself in comfort, calm and luxury at our charming bergerie, a conversion from a shepherd's old stone house! Nestled in the heart of the largest mimosa forest in Europe, overlooking the Cotes d'Azur and lower Alps, our tastefully designed retreat offers everything you need for an unforgettable tranquillity.We welcome up to 4 adults and have a small mezzanine for children.", "Check-in: 3:00 PM - 5:00 PM ,Checkout before 10:00 AM ,2 guests maximum", true, 43.5914m, 8, 6.8761m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 132m, 1, "No carbon monoxide alarm , No smoke alarm , Heights without rails or protectio", "New! The View: See to Mouintain (with pool)" },
                    { "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c", "Free cancellation before May 19. Cancel before check-in on May 24 for a partial refund.", "This is a guitar-shaped country house located in Icheon, a ceramic art village. It is a private house with a spacious terrace on the 3rd floor of the Sera Guitar Culture Center, famous for its unique appearance in the Icheon Ceramic Art Village, which blends in very well with nature.", "Check-in: 3:00 PM - 12:00 AM  , Checkout before 11:00  AM , 2 guests maximum", true, 33.9249m, 11, 18.4241m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 210m, 1, "Carbon monoxide alarm not reported , Smoke alarm , Must climb stairs", "Kai Cottage" },
                    { "efd964ab-dceb-4b96-b113-665c5684a102", "Free cancellation before Apr 26. Cancel before check-in on May 1 for a partial refund.", "Two hours from Bogotá on the Bogotá-Sasaima road, live the unique experience of staying in a tree eight meters high.Wake up to the chirping of birds and fall asleep to the sound of the stream below.Enjoy a five-star suite with all the amenities in the branches of the trees.The cabin has hot water, a mini-fridge, and the most spectacular view.", "Check-in brfore 3:00 PM , Checkout before 12:00 PM , 3 guests maximum", true, 4.96705m, 22, -74.43512m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 100m, 4, "carbon monoxide alarm  , No Smoke alarm , Nearby lake, river, other body of water", "The most spectacular treehouse in Colombia." },
                    { "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", "Free cancellation before Jun 3. Cancel before check-in on Jun 4 for a partial refund", "To give you the best experience of the authentic Bedouin life style, we will gather around the fire, cook our traditional food and tell you stories of our ancestors, while looking at the sky full of stars.Without a lie, this experience will be very special, if you used to cities and crowd in your everyday life.We created the space in a very simple, traditional and nomadic way. The Cave is inside the red rocks, waterproof and safe from all sides. Here you will have the whole Desert for yourself to get away from normal life, to relax, be in a quiet environment and meditate.", "Check-in: 3:00 PM - 5:00 PM ,Checkout before 10:00 AM ,2 guests maximum", true, 29.5726m, 7, 35.4186m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 220m, 1, "No carbon monoxide alarm , No smoke alarm , Heights without rails or protectio", "Wadi Rum Sunset Cave" },
                    { "f1e8be41-4fd5-47e4-8960-12d8f4afc273", "Free cancellation before May 5. Cancel before check-in on May 9 for a full refund.", "Welcome to our brand new one-bedroom flat offering incredible views of Business Bay canal and the iconic Burj Khalifa.", "Check-in brfore 1:00 Am , Checkout before 11:00 AM , 1 guests maximum", true, 25.2769m, 20, 55.2962m, "3dacdb51-fee9-4479-904c-cafe7dca22a7", 400m, 1, "carbon monoxide alarm  , Smoke alarm", "Cosy flat in the heart of Dubai" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CheckIn", "CheckOut", "CreatedAt", "PricePerNight", "PropertyId", "Status", "TotalFees", "UserId" },
                values: new object[,]
                {
                    { "0fe8f9f5-7751-460b-b39f-dab6946c0ba2", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2120m, "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", "Confirmed", 1900m, "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
                    { "438d19e1-66fc-4219-9e3d-0519c9c27332", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 5000m, "3e7f99ab-228a-4d90-91c4-6adf8c12e048", "Confirmed", 1200m, "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
                    { "49b69c8a-8b4b-4021-85f4-ff273b70c85d", new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 5000m, "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", "Confirmed", 1200m, "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
                    { "7b479ff7-22c5-46ad-85a3-204b502e5d0b", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3000m, "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", "Pending", 1900m, "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
                    { "7f6b0bb5-e99e-47c7-8d75-b5d46284e241", new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3000m, "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", "Pending", 89981m, "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
                    { "8a45a4b6-24ab-4a5b-8ef3-17b7de41295a", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3000m, "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", "Cancelled", 1900m, "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
                    { "b6d7b477-9b64-4a79-b7a3-b01c45378d5e", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000m, "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2", "Cancelled", 1200m, "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
                    { "d2bc71b9-d703-43fc-a90f-bf22f29a7b4e", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3000m, "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", "Confirmed", 2000m, "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
                    { "e42b9075-d67c-4b5f-8316-bde33ef7272a", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 5000m, "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", "Confirmed", 1200m, "4dacdb51-fee9-4479-904c-cafe7dca22a8" }
                });

            migrationBuilder.InsertData(
                table: "FavoriteProperties",
                columns: new[] { "PropertyId", "UserId" },
                values: new object[,]
                {
                    { "3e7f99ab-228a-4d90-91c4-6adf8c12e048", "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
                    { "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2", "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
                    { "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
                    { "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
                    { "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
                    { "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
                    { "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", "4dacdb51-fee9-4479-904c-cafe7dca22a8" },
                    { "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", "4dacdb51-fee9-4479-904c-cafe7dca22a8" }
                });

            migrationBuilder.InsertData(
                table: "PropertyAmenities",
                columns: new[] { "AmenityId", "PropertyId" },
                values: new object[,]
                {
                    { 39, "06dbae08-bc6b-4ca6-9162-3213784b9971" },
                    { 44, "06dbae08-bc6b-4ca6-9162-3213784b9971" },
                    { 47, "06dbae08-bc6b-4ca6-9162-3213784b9971" },
                    { 34, "0bb50f31-e322-4b76-97dd-6a7fcf585d33" },
                    { 35, "0bb50f31-e322-4b76-97dd-6a7fcf585d33" },
                    { 53, "0bb50f31-e322-4b76-97dd-6a7fcf585d33" },
                    { 30, "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08" },
                    { 39, "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08" },
                    { 42, "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08" },
                    { 17, "294e2751-203b-4beb-b21e-0bb96f082d7c" },
                    { 41, "294e2751-203b-4beb-b21e-0bb96f082d7c" },
                    { 43, "294e2751-203b-4beb-b21e-0bb96f082d7c" },
                    { 26, "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1" },
                    { 27, "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1" },
                    { 47, "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1" },
                    { 29, "2e3ed231-a2a6-4961-a1ba-f232d56c6f35" },
                    { 52, "2e3ed231-a2a6-4961-a1ba-f232d56c6f35" },
                    { 59, "2e3ed231-a2a6-4961-a1ba-f232d56c6f35" },
                    { 8, "3c0e361a-51df-4e03-b8d0-2d7601aa60f6" },
                    { 30, "3c0e361a-51df-4e03-b8d0-2d7601aa60f6" },
                    { 31, "3c0e361a-51df-4e03-b8d0-2d7601aa60f6" },
                    { 1, "3e7f99ab-228a-4d90-91c4-6adf8c12e048" },
                    { 7, "3e7f99ab-228a-4d90-91c4-6adf8c12e048" },
                    { 13, "3e7f99ab-228a-4d90-91c4-6adf8c12e048" },
                    { 22, "4b04a76a-1608-4a8f-b09c-8d9043b83e16" },
                    { 25, "4b04a76a-1608-4a8f-b09c-8d9043b83e16" },
                    { 34, "4b04a76a-1608-4a8f-b09c-8d9043b83e16" },
                    { 11, "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2" },
                    { 12, "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2" },
                    { 18, "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2" },
                    { 36, "52a8df7d-c0b2-4ee3-8369-9daed4885f9f" },
                    { 49, "52a8df7d-c0b2-4ee3-8369-9daed4885f9f" },
                    { 56, "52a8df7d-c0b2-4ee3-8369-9daed4885f9f" },
                    { 1, "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa" },
                    { 8, "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa" },
                    { 16, "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa" },
                    { 14, "763e6c5f-1ad1-4071-b0e6-55e924624198" },
                    { 46, "763e6c5f-1ad1-4071-b0e6-55e924624198" },
                    { 53, "763e6c5f-1ad1-4071-b0e6-55e924624198" },
                    { 5, "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4" },
                    { 9, "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4" },
                    { 10, "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4" },
                    { 2, "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3" },
                    { 14, "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3" },
                    { 21, "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3" },
                    { 36, "a555515a-ff8a-4741-b0a4-db9be729198e" },
                    { 38, "a555515a-ff8a-4741-b0a4-db9be729198e" },
                    { 41, "a555515a-ff8a-4741-b0a4-db9be729198e" },
                    { 5, "c10d2d46-869a-46bc-a46d-90bdd958c252" },
                    { 37, "c10d2d46-869a-46bc-a46d-90bdd958c252" },
                    { 40, "c10d2d46-869a-46bc-a46d-90bdd958c252" },
                    { 19, "c150e428-1c9a-43a2-be07-f4366875f1ce" },
                    { 50, "c150e428-1c9a-43a2-be07-f4366875f1ce" },
                    { 58, "c150e428-1c9a-43a2-be07-f4366875f1ce" },
                    { 26, "c5c0d4db-b048-4ee4-8835-344900fd35b2" },
                    { 32, "c5c0d4db-b048-4ee4-8835-344900fd35b2" },
                    { 33, "c5c0d4db-b048-4ee4-8835-344900fd35b2" },
                    { 1, "cc4e48ea-ca54-4d32-a448-3c2c9d14f936" },
                    { 3, "cc4e48ea-ca54-4d32-a448-3c2c9d14f936" },
                    { 6, "cc4e48ea-ca54-4d32-a448-3c2c9d14f936" },
                    { 19, "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f" },
                    { 20, "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f" },
                    { 24, "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f" },
                    { 12, "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c" },
                    { 28, "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c" },
                    { 29, "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c" },
                    { 7, "efd964ab-dceb-4b96-b113-665c5684a102" },
                    { 48, "efd964ab-dceb-4b96-b113-665c5684a102" },
                    { 54, "efd964ab-dceb-4b96-b113-665c5684a102" },
                    { 4, "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7" },
                    { 17, "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7" },
                    { 23, "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7" },
                    { 22, "f1e8be41-4fd5-47e4-8960-12d8f4afc273" },
                    { 45, "f1e8be41-4fd5-47e4-8960-12d8f4afc273" },
                    { 51, "f1e8be41-4fd5-47e4-8960-12d8f4afc273" }
                });

            migrationBuilder.InsertData(
                table: "PropertyAvailabilities",
                columns: new[] { "Id", "EndDate", "IsAvailable", "PropertyId", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", new DateTime(2025, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", new DateTime(2025, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "3e7f99ab-228a-4d90-91c4-6adf8c12e048", new DateTime(2025, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "3e7f99ab-228a-4d90-91c4-6adf8c12e048", new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2", new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2", new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", new DateTime(2025, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "3e7f99ab-228a-4d90-91c4-6adf8c12e048", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, new DateTime(2025, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2", new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, new DateTime(2025, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "4b04a76a-1608-4a8f-b09c-8d9043b83e16", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "4b04a76a-1608-4a8f-b09c-8d9043b83e16", new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "4b04a76a-1608-4a8f-b09c-8d9043b83e16", new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1", new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1", new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31, new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c", new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32, new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 33, new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c", new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "3c0e361a-51df-4e03-b8d0-2d7601aa60f6", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35, new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "3c0e361a-51df-4e03-b8d0-2d7601aa60f6", new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 36, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "3c0e361a-51df-4e03-b8d0-2d7601aa60f6", new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37, new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "c5c0d4db-b048-4ee4-8835-344900fd35b2", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38, new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "c5c0d4db-b048-4ee4-8835-344900fd35b2", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 39, new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "c5c0d4db-b048-4ee4-8835-344900fd35b2", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40, new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "0bb50f31-e322-4b76-97dd-6a7fcf585d33", new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 41, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "0bb50f31-e322-4b76-97dd-6a7fcf585d33", new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 42, new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "0bb50f31-e322-4b76-97dd-6a7fcf585d33", new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43, new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "a555515a-ff8a-4741-b0a4-db9be729198e", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 44, new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "a555515a-ff8a-4741-b0a4-db9be729198e", new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 45, new DateTime(2025, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "a555515a-ff8a-4741-b0a4-db9be729198e", new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 46, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "c10d2d46-869a-46bc-a46d-90bdd958c252", new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 47, new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "c10d2d46-869a-46bc-a46d-90bdd958c252", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 48, new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "c10d2d46-869a-46bc-a46d-90bdd958c252", new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 49, new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 50, new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08", new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 51, new DateTime(2025, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08", new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 52, new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "294e2751-203b-4beb-b21e-0bb96f082d7c", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 53, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "294e2751-203b-4beb-b21e-0bb96f082d7c", new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 54, new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "294e2751-203b-4beb-b21e-0bb96f082d7c", new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 55, new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "06dbae08-bc6b-4ca6-9162-3213784b9971", new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 56, new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "06dbae08-bc6b-4ca6-9162-3213784b9971", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 57, new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "06dbae08-bc6b-4ca6-9162-3213784b9971", new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 58, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "f1e8be41-4fd5-47e4-8960-12d8f4afc273", new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 59, new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "f1e8be41-4fd5-47e4-8960-12d8f4afc273", new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 60, new DateTime(2025, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "f1e8be41-4fd5-47e4-8960-12d8f4afc273", new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 61, new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "763e6c5f-1ad1-4071-b0e6-55e924624198", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 62, new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "763e6c5f-1ad1-4071-b0e6-55e924624198", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 63, new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "763e6c5f-1ad1-4071-b0e6-55e924624198", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 64, new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "efd964ab-dceb-4b96-b113-665c5684a102", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 65, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "efd964ab-dceb-4b96-b113-665c5684a102", new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 66, new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "efd964ab-dceb-4b96-b113-665c5684a102", new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 67, new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "52a8df7d-c0b2-4ee3-8369-9daed4885f9f", new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 68, new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "52a8df7d-c0b2-4ee3-8369-9daed4885f9f", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 69, new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "52a8df7d-c0b2-4ee3-8369-9daed4885f9f", new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 70, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "c150e428-1c9a-43a2-be07-f4366875f1ce", new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 71, new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "c150e428-1c9a-43a2-be07-f4366875f1ce", new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 72, new DateTime(2025, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "c150e428-1c9a-43a2-be07-f4366875f1ce", new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 73, new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "2e3ed231-a2a6-4961-a1ba-f232d56c6f35", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 74, new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "2e3ed231-a2a6-4961-a1ba-f232d56c6f35", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 75, new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "2e3ed231-a2a6-4961-a1ba-f232d56c6f35", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "PropertyFees",
                columns: new[] { "Id", "Amount", "Name", "PropertyId" },
                values: new object[,]
                {
                    { 1, 1212.09m, "Cleaning Fee", "cc4e48ea-ca54-4d32-a448-3c2c9d14f936" },
                    { 2, 442.09m, "Extra Guest Fee", "cc4e48ea-ca54-4d32-a448-3c2c9d14f936" },
                    { 3, 600m, "Pet Fee", "cc4e48ea-ca54-4d32-a448-3c2c9d14f936" },
                    { 4, 1200m, "Cleaning Fee", "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4" },
                    { 5, 600m, "Pet Fee", "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4" },
                    { 6, 950.50m, "Cleaning Fee", "3e7f99ab-228a-4d90-91c4-6adf8c12e048" },
                    { 7, 900.12m, "Cleaning Fee", "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa" },
                    { 8, 330.00m, "Extra Guest Fee", "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2" },
                    { 9, 442.09m, "Pet Fee", "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3" },
                    { 10, 800.75m, "Cleaning Fee", "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7" },
                    { 11, 113.09m, "Cleaning Fee", "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f" },
                    { 12, 510.00m, "Cleaning Fee", "4b04a76a-1608-4a8f-b09c-8d9043b83e16" },
                    { 13, 250.00m, "Pet Fee", "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1" },
                    { 14, 789.99m, "Cleaning Fee", "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c" },
                    { 15, 199.99m, "Extra Guest Fee", "3c0e361a-51df-4e03-b8d0-2d7601aa60f6" },
                    { 16, 450.00m, "Cleaning Fee", "c5c0d4db-b048-4ee4-8835-344900fd35b2" },
                    { 17, 320.00m, "Pet Fee", "0bb50f31-e322-4b76-97dd-6a7fcf585d33" },
                    { 18, 670.00m, "Cleaning Fee", "a555515a-ff8a-4741-b0a4-db9be729198e" },
                    { 19, 275.50m, "Extra Guest Fee", "c10d2d46-869a-46bc-a46d-90bdd958c252" },
                    { 20, 390.00m, "Cleaning Fee", "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08" },
                    { 21, 425.99m, "Cleaning Fee", "294e2751-203b-4beb-b21e-0bb96f082d7c" },
                    { 22, 515.49m, "Pet Fee", "06dbae08-bc6b-4ca6-9162-3213784b9971" },
                    { 23, 398.89m, "Extra Guest Fee", "f1e8be41-4fd5-47e4-8960-12d8f4afc273" },
                    { 24, 300.00m, "Cleaning Fee", "763e6c5f-1ad1-4071-b0e6-55e924624198" },
                    { 25, 345.00m, "Cleaning Fee", "efd964ab-dceb-4b96-b113-665c5684a102" },
                    { 26, 410.00m, "Pet Fee", "52a8df7d-c0b2-4ee3-8369-9daed4885f9f" },
                    { 27, 289.00m, "Extra Guest Fee", "c150e428-1c9a-43a2-be07-f4366875f1ce" },
                    { 28, 378.00m, "Cleaning Fee", "2e3ed231-a2a6-4961-a1ba-f232d56c6f35" }
                });

            migrationBuilder.InsertData(
                table: "PropertyGuests",
                columns: new[] { "GuestTypeId", "PropertyId", "GuestCount" },
                values: new object[,]
                {
                    { 3, "06dbae08-bc6b-4ca6-9162-3213784b9971", 4 },
                    { 2, "0bb50f31-e322-4b76-97dd-6a7fcf585d33", 4 },
                    { 1, "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08", 5 },
                    { 2, "294e2751-203b-4beb-b21e-0bb96f082d7c", 2 },
                    { 1, "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1", 5 },
                    { 1, "2e3ed231-a2a6-4961-a1ba-f232d56c6f35", 2 },
                    { 1, "3c0e361a-51df-4e03-b8d0-2d7601aa60f6", 4 },
                    { 1, "3e7f99ab-228a-4d90-91c4-6adf8c12e048", 3 },
                    { 1, "4b04a76a-1608-4a8f-b09c-8d9043b83e16", 3 },
                    { 1, "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2", 5 },
                    { 3, "52a8df7d-c0b2-4ee3-8369-9daed4885f9f", 2 },
                    { 1, "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", 2 },
                    { 1, "763e6c5f-1ad1-4071-b0e6-55e924624198", 3 },
                    { 1, "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", 4 },
                    { 1, "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", 2 },
                    { 3, "a555515a-ff8a-4741-b0a4-db9be729198e", 1 },
                    { 4, "c10d2d46-869a-46bc-a46d-90bdd958c252", 3 },
                    { 4, "c150e428-1c9a-43a2-be07-f4366875f1ce", 4 },
                    { 1, "c5c0d4db-b048-4ee4-8835-344900fd35b2", 2 },
                    { 1, "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", 1 },
                    { 1, "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", 1 },
                    { 1, "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c", 2 },
                    { 2, "efd964ab-dceb-4b96-b113-665c5684a102", 1 },
                    { 1, "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", 4 },
                    { 4, "f1e8be41-4fd5-47e4-8960-12d8f4afc273", 2 }
                });

            migrationBuilder.InsertData(
                table: "PropertyPhotos",
                columns: new[] { "PhotoId", "PropertyId", "TouchedAt" },
                values: new object[,]
                {
                    { "0184da01-3f04-431a-821b-863db48eee6b", "3c0e361a-51df-4e03-b8d0-2d7601aa60f6", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "08f3b524-1ff0-4d1f-a4f9-a50c0d6ee717", "f1e8be41-4fd5-47e4-8960-12d8f4afc273", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "0f18b242-e627-45eb-a22d-516722b7c78c", "06dbae08-bc6b-4ca6-9162-3213784b9971", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "11010e1b-3c99-4d25-a176-9b826b19ec88", "4b04a76a-1608-4a8f-b09c-8d9043b83e16", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "1389e44f-240f-4eed-bde3-93623d7c41d1", "f1e8be41-4fd5-47e4-8960-12d8f4afc273", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "160b2604-8211-42b5-9f78-4360d5a71ee9", "c5c0d4db-b048-4ee4-8835-344900fd35b2", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "1cc7082f-8324-4888-b903-9d8ed2ffd144", "4b04a76a-1608-4a8f-b09c-8d9043b83e16", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "1d0aa7e5-30b6-42f6-aa21-11fed6d12c9a", "c5c0d4db-b048-4ee4-8835-344900fd35b2", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "1fb978f8-fb49-4f38-8acb-345be5c86bc7", "52a8df7d-c0b2-4ee3-8369-9daed4885f9f", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "26d418bb-0f90-4f3c-b339-7dd5c31b5e99", "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "2ac68b52-e7b6-4bb7-9f8e-49aa7f2b2b6c", "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "2cf95d6d-63ae-4b97-8101-c6c5e8227b6d", "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "2f50cb6f-8aeb-4428-8279-7c3a11d18232", "2e3ed231-a2a6-4961-a1ba-f232d56c6f35", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "301f7e01-cc25-48ed-90aa-fafe16fce3b5", "06dbae08-bc6b-4ca6-9162-3213784b9971", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "303c12b0-baca-42d4-824e-d84b940d317a", "294e2751-203b-4beb-b21e-0bb96f082d7c", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "3588517b-0a71-4d29-ad8c-906a8e545d00", "c10d2d46-869a-46bc-a46d-90bdd958c252", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "3777d149-0028-4ea1-ba62-db41d33939f5", "efd964ab-dceb-4b96-b113-665c5684a102", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "3cb5e765-921f-4e0e-97be-b6d1e4c762cf", "c150e428-1c9a-43a2-be07-f4366875f1ce", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "4b0f81f1-9bc0-45c6-988e-1a4fd270b3e0", "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "4c376b94-d74f-4472-b1a5-4c3d51df56d8", "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "4dfe3d56-2d34-4a6b-9cb5-f7a5a2dd8c28", "3e7f99ab-228a-4d90-91c4-6adf8c12e048", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "51d1e109-dccf-45fd-9f15-bbd3c0b7fcd5", "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "55a42f5d-4934-41df-8077-4ea9654c8d4f", "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "5b742ed2-28d9-4e3b-8125-6e9c4587a0d3", "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "5c8fa3e9-2590-44d4-8e36-ee7f3c526b37", "efd964ab-dceb-4b96-b113-665c5684a102", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "5e2e82a1-4893-4a63-9375-d73f7a09d7c5", "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "68b3f994-ed3d-461e-89b2-13ebe89d53b6", "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "69c6c01e-65b3-4cf7-bbc7-2e94272b658a", "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "6c54a231-b88f-409f-b5d5-170180930186", "3e7f99ab-228a-4d90-91c4-6adf8c12e048", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "6c79893d-f97f-4fc6-b0c3-4ebfcab3f85f", "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "6f572d60-4465-40a4-8b63-7bb2eb876cbd", "efd964ab-dceb-4b96-b113-665c5684a102", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "7a18064f-b6cb-4d58-a51b-0e8a74eac7a4", "3e7f99ab-228a-4d90-91c4-6adf8c12e048", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "7ae244c8-42a8-422c-9be6-b809c1b427f6", "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "7d861c0c-011d-4b2a-8ce5-f5b1f0b81d01", "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "82da2e46-5e65-4ad8-97e9-ad10fdd63171", "763e6c5f-1ad1-4071-b0e6-55e924624198", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "84d8e12a-4754-4825-b0fc-2b43981f6ba0", "0bb50f31-e322-4b76-97dd-6a7fcf585d33", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "87668883-00e2-4d99-9dea-b612fb1f09fb", "3c0e361a-51df-4e03-b8d0-2d7601aa60f6", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "89f65612-5023-489e-9604-2f01074abf0c", "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "90cd8c79-cc01-4edf-85b8-9931cd3fc772", "c10d2d46-869a-46bc-a46d-90bdd958c252", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "9201fad7-d63f-4dbf-84f1-adb25c451e9e", "294e2751-203b-4beb-b21e-0bb96f082d7c", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "95cde2b1-305e-4c13-9293-8c4c8f7c8b9f", "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "98a76538-918f-4e60-9c01-b364e0e1891f", "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "9b0d97e4-6dad-4e5b-893c-38aaff4a50e2", "294e2751-203b-4beb-b21e-0bb96f082d7c", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "9ea371c2-fefe-423f-953c-c744a33d5fb9", "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "9f90c24f-0a95-46a3-a2e2-a0688c460a23", "c5c0d4db-b048-4ee4-8835-344900fd35b2", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "a05afc7a-9127-4a33-839c-908e1f47a4ae", "a555515a-ff8a-4741-b0a4-db9be729198e", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "a2529147-026b-4b8b-a811-cb18989a8129", "06dbae08-bc6b-4ca6-9162-3213784b9971", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "a4c0d40d-e90e-4b14-8a2a-5ac0212be9b1", "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "a6d2fafb-6490-4f6f-a4c7-f42fdde98bf2", "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "a73860df-b173-4d4d-b834-124f19d93a20e", "c150e428-1c9a-43a2-be07-f4366875f1ce", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "aac5407d-a994-4c3e-a1ff-b7646d79162a", "a555515a-ff8a-4741-b0a4-db9be729198e", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "ab39dc17-5108-425b-8350-1995323ba1a1", "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "aca8279a-04bd-4277-8370-1338beb17581", "3c0e361a-51df-4e03-b8d0-2d7601aa60f6", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "b01df5ef-3951-4e4c-80c5-00e10029a682", "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "b21f8f4f-6d95-4f60-81b4-56d2ef017a08", "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "b455bb0a-69a3-4024-b5fa-5a49323e58fd", "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "ba47797b-da79-47a0-8014-48e5422f0500", "52a8df7d-c0b2-4ee3-8369-9daed4885f9f", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "caf5622f-99a6-4927-a913-48d66437de5d", "0bb50f31-e322-4b76-97dd-6a7fcf585d33", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "ce9e31d6-6553-4214-8b94-fb9c8f3065ed", "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "da2afaf9-b1df-4daf-bb44-3d6a79be4a17", "4b04a76a-1608-4a8f-b09c-8d9043b83e16", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "da734851-2db8-4541-a788-b675b7560eec", "c10d2d46-869a-46bc-a46d-90bdd958c252", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "dc16e3d2-16ed-4ff5-b9c2-27a1e8b5ccbe", "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "e019ead5-3b99-4f78-a84b-23b34ba27e26", "52a8df7d-c0b2-4ee3-8369-9daed4885f9f", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "e0f27e50-9e45-489b-90d6-f62211f67f12", "0bb50f31-e322-4b76-97dd-6a7fcf585d33", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "e34a2808-38df-4e47-8c3e-d6e3f2712f11", "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "e3d6bbae-2087-4269-b6ca-784e3301cce0", "a555515a-ff8a-4741-b0a4-db9be729198e", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "e4a28523-f13c-431a-8af5-2ebd307f1a85", "763e6c5f-1ad1-4071-b0e6-55e924624198", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "e56c967f-ee64-4e53-b0e6-1b1342baf2da", "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "ec263c4f-fcc3-4d72-805e-d0b116e2cdd7", "2e3ed231-a2a6-4961-a1ba-f232d56c6f35", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "f3885b77-0f9e-4ec3-9b3e-cbc194a07d7f", "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "f3db201e-fddd-4278-9beb-96863dde2f0f", "763e6c5f-1ad1-4071-b0e6-55e924624198", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "f4b528d3-1204-4ccc-af05-2a39346d7ace", "c150e428-1c9a-43a2-be07-f4366875f1ce", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "fbb7ade9-39b8-4b3b-abb5-b38fc1f70471", "2e3ed231-a2a6-4961-a1ba-f232d56c6f35", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "fbc177de-bf4c-4b75-a1f6-884d05ce6c9f", "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "ff840d01-6eab-45fb-a911-674725a89003", "f1e8be41-4fd5-47e4-8960-12d8f4afc273", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "PropertySpaces",
                columns: new[] { "Id", "Name", "PropertyId", "PropertySpaceTypeId" },
                values: new object[,]
                {
                    { "14a66729-9580-472b-9438-dfc7e2440c95", "Office", "f1cc1b4c-b674-4a1a-89ee-5f7b4d44d2f7", 6 },
                    { "188b8c66-66fa-42a2-944c-4fd3f048250c", "Closet", "c10d2d46-869a-46bc-a46d-90bdd958c252", 13 },
                    { "1954cfa5-9c89-41a7-a6be-41c71b34efc9", "Sunroom", "0bb50f31-e322-4b76-97dd-6a7fcf585d33", 12 }
                });

            migrationBuilder.InsertData(
                table: "PropertySpaces",
                columns: new[] { "Id", "IsShared", "Name", "PropertyId", "PropertySpaceTypeId" },
                values: new object[,]
                {
                    { "1cc7112f-3bb5-4265-8e0a-b305274c0410", true, "Gym", "ef3b2df2-e539-4cb9-8eb6-4eeb833e694c", 6 },
                    { "29ac2c68-b4b4-45b8-918a-fbdf11660d7e", true, "Playroom", "52a8df7d-c0b2-4ee3-8369-9daed4885f9f", 14 },
                    { "30baf72d-9d00-4f3e-9405-2261d6f0dd76", true, "Study", "f1e8be41-4fd5-47e4-8960-12d8f4afc273", 6 },
                    { "325e05d6-cc5d-4140-b1bc-d96fc52d86b3", true, "Living Room", "3e7f99ab-228a-4d90-91c4-6adf8c12e048", 4 },
                    { "49f23d20-c9ae-4a77-9734-1886d424cb77", true, "Laundry Room", "1adca40b-b8ff-4cea-b6e4-8e5f40d29c08", 7 },
                    { "5eb1c7e5-efb6-4b3c-983f-d278c1c086e7", true, "Reception", "294e2751-203b-4beb-b21e-0bb96f082d7c", 8 }
                });

            migrationBuilder.InsertData(
                table: "PropertySpaces",
                columns: new[] { "Id", "Name", "PropertyId", "PropertySpaceTypeId" },
                values: new object[] { "62b66c76-60d1-4b4b-8e97-bfd0338ea05a", "Bathroom", "5ca2f710-3c1f-4966-a924-7bcdf5ce57aa", 2 });

            migrationBuilder.InsertData(
                table: "PropertySpaces",
                columns: new[] { "Id", "IsShared", "Name", "PropertyId", "PropertySpaceTypeId" },
                values: new object[,]
                {
                    { "6b09c3a9-e319-45d0-a253-b5d6f4f9de3a", true, "Porch", "763e6c5f-1ad1-4071-b0e6-55e924624198", 9 },
                    { "6c67a41a-8274-4ad0-864e-20fd4866b2d4", true, "Theater", "3c0e361a-51df-4e03-b8d0-2d7601aa60f6", 15 },
                    { "726d598e-c948-41b6-8cc3-c7e1aa4a51e4", true, "Dining Room", "a43ecbfa-7b0a-4f6b-9c88-987be3c4e3d3", 5 },
                    { "846b07ee-bb17-4c94-82df-99f1f7643ea3", true, "Pantry", "c5c0d4db-b048-4ee4-8835-344900fd35b2", 11 }
                });

            migrationBuilder.InsertData(
                table: "PropertySpaces",
                columns: new[] { "Id", "Name", "PropertyId", "PropertySpaceTypeId" },
                values: new object[] { "8cf76f1f-7f39-4d78-bcc7-2a2a34db54b3", "Utility Room", "c150e428-1c9a-43a2-be07-f4366875f1ce", 15 });

            migrationBuilder.InsertData(
                table: "PropertySpaces",
                columns: new[] { "Id", "IsShared", "Name", "PropertyId", "PropertySpaceTypeId" },
                values: new object[] { "96f6a377-d586-44a2-acc7-fc45c10d999c", true, "Library", "2ab6e4d1-79b9-4dba-9109-22ef75a29ff1", 6 });

            migrationBuilder.InsertData(
                table: "PropertySpaces",
                columns: new[] { "Id", "Name", "PropertyId", "PropertySpaceTypeId" },
                values: new object[] { "9f5f9e6e-0d79-41ad-86a1-06cbff2d0e92", "Guest Room", "06dbae08-bc6b-4ca6-9162-3213784b9971", 1 });

            migrationBuilder.InsertData(
                table: "PropertySpaces",
                columns: new[] { "Id", "IsShared", "Name", "PropertyId", "PropertySpaceTypeId" },
                values: new object[] { "b09ce60b-b66b-47df-9985-41d1e7f6b254", true, "Balcony", "4e3d342-8e8d-4f1d-8123-2d09cb92b6a2", 9 });

            migrationBuilder.InsertData(
                table: "PropertySpaces",
                columns: new[] { "Id", "Name", "PropertyId", "PropertySpaceTypeId" },
                values: new object[,]
                {
                    { "c6ae89de-0d1a-4e5a-9230-8ef6617a3b53", "Hallway", "a555515a-ff8a-4741-b0a4-db9be729198e", 10 },
                    { "c8f09e6f-8c82-4026-b3ec-23be0a378a56", "Storage", "4b04a76a-1608-4a8f-b09c-8d9043b83e16", 16 }
                });

            migrationBuilder.InsertData(
                table: "PropertySpaces",
                columns: new[] { "Id", "IsShared", "Name", "PropertyId", "PropertySpaceTypeId" },
                values: new object[] { "d20a85b2-4019-4714-a63e-e017b4be4e3e", true, "Kitchen", "8e95f4b1-dc1d-4b4d-8102-09b7fbb88ec4", 3 });

            migrationBuilder.InsertData(
                table: "PropertySpaces",
                columns: new[] { "Id", "Name", "PropertyId", "PropertySpaceTypeId" },
                values: new object[,]
                {
                    { "d2e5f682-06d0-40e7-a1e7-002b958d8048", "Workshop", "efd964ab-dceb-4b96-b113-665c5684a102", 13 },
                    { "daae3bd2-707e-4374-9b6c-5703f9789c7f", "Bedroom", "cc4e48ea-ca54-4d32-a448-3c2c9d14f936", 1 }
                });

            migrationBuilder.InsertData(
                table: "PropertySpaces",
                columns: new[] { "Id", "IsShared", "Name", "PropertyId", "PropertySpaceTypeId" },
                values: new object[] { "f8c7fef3-70f4-4650-baa6-f93db77dfd92", true, "Game Room", "d8eecb1f-5583-4d64-a7dc-5aef5e2c498f", 14 });

            migrationBuilder.InsertData(
                table: "BookingGuests",
                columns: new[] { "BookingId", "GuestTypeId", "GuestCount" },
                values: new object[,]
                {
                    { "0fe8f9f5-7751-460b-b39f-dab6946c0ba2", 2, 1 },
                    { "438d19e1-66fc-4219-9e3d-0519c9c27332", 3, 2 },
                    { "49b69c8a-8b4b-4021-85f4-ff273b70c85d", 2, 3 },
                    { "7b479ff7-22c5-46ad-85a3-204b502e5d0b", 4, 2 },
                    { "7f6b0bb5-e99e-47c7-8d75-b5d46284e241", 1, 3 },
                    { "8a45a4b6-24ab-4a5b-8ef3-17b7de41295a", 1, 2 },
                    { "b6d7b477-9b64-4a79-b7a3-b01c45378d5e", 2, 1 },
                    { "d2bc71b9-d703-43fc-a90f-bf22f29a7b4e", 2, 2 },
                    { "e42b9075-d67c-4b5f-8316-bde33ef7272a", 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "PropertySpaceItems",
                columns: new[] { "Id", "PropertySpaceId", "PropertySpaceItemTypeId", "Quantity" },
                values: new object[,]
                {
                    { 1, "daae3bd2-707e-4374-9b6c-5703f9789c7f", 2, 1 },
                    { 2, "d20a85b2-4019-4714-a63e-e017b4be4e3e", 2, 2 },
                    { 3, "325e05d6-cc5d-4140-b1bc-d96fc52d86b3", 10, 1 },
                    { 4, "62b66c76-60d1-4b4b-8e97-bfd0338ea05a", 7, 2 },
                    { 5, "b09ce60b-b66b-47df-9985-41d1e7f6b254", 4, 2 },
                    { 6, "726d598e-c948-41b6-8cc3-c7e1aa4a51e4", 25, 2 },
                    { 7, "14a66729-9580-472b-9438-dfc7e2440c95", 13, 1 },
                    { 8, "f8c7fef3-70f4-4650-baa6-f93db77dfd92", 16, 1 },
                    { 9, "c8f09e6f-8c82-4026-b3ec-23be0a378a56", 39, 1 },
                    { 10, "96f6a377-d586-44a2-acc7-fc45c10d999c", 42, 1 },
                    { 11, "1cc7112f-3bb5-4265-8e0a-b305274c0410", 18, 2 },
                    { 12, "6c67a41a-8274-4ad0-864e-20fd4866b2d4", 12, 1 },
                    { 13, "846b07ee-bb17-4c94-82df-99f1f7643ea3", 5, 1 },
                    { 14, "1954cfa5-9c89-41a7-a6be-41c71b34efc9", 19, 3 },
                    { 15, "c6ae89de-0d1a-4e5a-9230-8ef6617a3b53", 7, 2 },
                    { 16, "49f23d20-c9ae-4a77-9734-1886d424cb77", 6, 1 },
                    { 17, "5eb1c7e5-efb6-4b3c-983f-d278c1c086e7", 8, 2 },
                    { 18, "9f5f9e6e-0d79-41ad-86a1-06cbff2d0e92", 1, 1 },
                    { 19, "30baf72d-9d00-4f3e-9405-2261d6f0dd76", 3, 2 },
                    { 20, "6b09c3a9-e319-45d0-a253-b5d6f4f9de3a", 9, 1 },
                    { 21, "d2e5f682-06d0-40e7-a1e7-002b958d8048", 11, 2 },
                    { 22, "29ac2c68-b4b4-45b8-918a-fbdf11660d7e", 14, 1 },
                    { 23, "8cf76f1f-7f39-4d78-bcc7-2a2a34db54b3", 15, 1 },
                    { 24, "8cf76f1f-7f39-4d78-bcc7-2a2a34db54b3", 12, 2 },
                    { 25, "14a66729-9580-472b-9438-dfc7e2440c95", 3, 1 },
                    { 26, "5eb1c7e5-efb6-4b3c-983f-d278c1c086e7", 1, 1 },
                    { 27, "9f5f9e6e-0d79-41ad-86a1-06cbff2d0e92", 2, 1 },
                    { 28, "1954cfa5-9c89-41a7-a6be-41c71b34efc9", 9, 3 },
                    { 29, "8cf76f1f-7f39-4d78-bcc7-2a2a34db54b3", 6, 1 },
                    { 30, "6c67a41a-8274-4ad0-864e-20fd4866b2d4", 5, 2 },
                    { 31, "6b09c3a9-e319-45d0-a253-b5d6f4f9de3a", 7, 1 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Accuracy", "BookingId", "CheckIn", "Cleanliness", "Comment", "Communication", "CreatedAt", "Location", "Value" },
                values: new object[,]
                {
                    { "2fca2c7e-263b-4d7e-99e7-0c1c3ad2aa08", 3.0m, "438d19e1-66fc-4219-9e3d-0519c9c27332", 2.5m, 3.5m, "Could be better", 4.0m, new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3.0m, 3.0m },
                    { "66d2f0d9-1f1f-4a02-81d6-0ecabc5215e6", 4.0m, "7f6b0bb5-e99e-47c7-8d75-b5d46284e241", 5.0m, 4.5m, "Great stay overall", 4.5m, new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 4.0m, 5.0m },
                    { "72b3d68d-234a-4ed7-b7f7-e07fc82f58ef", 4.0m, "b6d7b477-9b64-4a79-b7a3-b01c45378d5e", 3.5m, 4.0m, "Good value for money", 4.5m, new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 4.0m, 4.0m },
                    { "84c03e84-cd8b-4dbf-a0f4-48ed3dd0b0aa", 5.0m, "8a45a4b6-24ab-4a5b-8ef3-17b7de41295a", 5.0m, 5.0m, "Excellent experience", 5.0m, new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0m, 5.0m },
                    { "a54b86b1-65e2-426b-81ef-c65c71e5b8d0", 4.5m, "49b69c8a-8b4b-4021-85f4-ff273b70c85d", 4.0m, 5.0m, "Comfortable and clean", 5.0m, new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 4.5m, 4.5m },
                    { "e62cd505-8d60-430b-8b52-16d40902a303", 4.5m, "7b479ff7-22c5-46ad-85a3-204b502e5d0b", 4.0m, 4.5m, "Very good host", 5.0m, new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 4.5m, 4.5m },
                    { "fca2e08b-0436-4f3f-8261-f69cf3eaa579", 3.5m, "e42b9075-d67c-4b5f-8316-bde33ef7272a", 3.0m, 3.0m, "Average experience", 3.5m, new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3.0m, 3.0m },
                    { "ffc234ae-2820-4fd6-b9d7-6b315d91a790", 5.0m, "0fe8f9f5-7751-460b-b39f-dab6946c0ba2", 5.0m, 5.0m, "Perfect location", 5.0m, new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0m, 5.0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_CategoryId",
                table: "Amenities",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BookingGuests_BookingId",
                table: "BookingGuests",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingGuests_GuestTypeId",
                table: "BookingGuests",
                column: "GuestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CheckIn",
                table: "Bookings",
                column: "CheckIn");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CheckOut",
                table: "Bookings",
                column: "CheckOut");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PropertyId",
                table: "Bookings",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Status",
                table: "Bookings",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_User1Id",
                table: "Conversations",
                column: "User1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_User2Id",
                table: "Conversations",
                column: "User2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_RegionId",
                table: "Countries",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProperties_UserId",
                table: "FavoriteProperties",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HostUpgradeRequests_ApprovedBy",
                table: "HostUpgradeRequests",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_HostUpgradeRequests_BackPhotoId",
                table: "HostUpgradeRequests",
                column: "BackPhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostUpgradeRequests_FrontPhotoId",
                table: "HostUpgradeRequests",
                column: "FrontPhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostUpgradeRequests_Status",
                table: "HostUpgradeRequests",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_HostUpgradeRequests_UserId",
                table: "HostUpgradeRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CountryId",
                table: "Locations",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId1",
                table: "Notifications",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Status",
                table: "Payments",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StripePaymentIntentId",
                table: "Payments",
                column: "StripePaymentIntentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StripeSessionId",
                table: "Payments",
                column: "StripeSessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_LocationId",
                table: "Properties",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerId",
                table: "Properties",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAmenities_AmenityId",
                table: "PropertyAmenities",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAvailabilities_PropertyId",
                table: "PropertyAvailabilities",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFees_PropertyId",
                table: "PropertyFees",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyGuests_GuestTypeId",
                table: "PropertyGuests",
                column: "GuestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyPhotos_PropertyId",
                table: "PropertyPhotos",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySpaceItems_PropertySpaceId",
                table: "PropertySpaceItems",
                column: "PropertySpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySpaceItems_PropertySpaceItemTypeId",
                table: "PropertySpaceItems",
                column: "PropertySpaceItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySpaceItemTypes_PropertySpaceTypeId",
                table: "PropertySpaceItemTypes",
                column: "PropertySpaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySpaces_PropertyId",
                table: "PropertySpaces",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySpaces_PropertySpaceTypeId",
                table: "PropertySpaces",
                column: "PropertySpaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookingId",
                table: "Reviews",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CreatedAt",
                table: "Reviews",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_CountryId",
                table: "UserProfiles",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_PhotoId",
                table: "UserProfiles",
                column: "PhotoId",
                unique: true,
                filter: "[PhotoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Violations_ReportedById1",
                table: "Violations",
                column: "ReportedById1");

            migrationBuilder.CreateIndex(
                name: "IX_Violations_ReportedPropertyId1",
                table: "Violations",
                column: "ReportedPropertyId1");

            migrationBuilder.CreateIndex(
                name: "IX_Violations_ReportedUserId1",
                table: "Violations",
                column: "ReportedUserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BookingGuests");

            migrationBuilder.DropTable(
                name: "FavoriteProperties");

            migrationBuilder.DropTable(
                name: "HostUpgradeRequests");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PropertyAmenities");

            migrationBuilder.DropTable(
                name: "PropertyAvailabilities");

            migrationBuilder.DropTable(
                name: "PropertyFees");

            migrationBuilder.DropTable(
                name: "PropertyGuests");

            migrationBuilder.DropTable(
                name: "PropertyPhotos");

            migrationBuilder.DropTable(
                name: "PropertySpaceItems");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Violations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.DropTable(
                name: "GuestTypes");

            migrationBuilder.DropTable(
                name: "PropertySpaceItemTypes");

            migrationBuilder.DropTable(
                name: "PropertySpaces");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "FileUploads");

            migrationBuilder.DropTable(
                name: "AmenityCategories");

            migrationBuilder.DropTable(
                name: "PropertySpaceTypes");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "PropertyTypes");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}

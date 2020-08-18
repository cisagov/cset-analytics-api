using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CsetAnalytics.DomainModels.Migrations
{
    public partial class firstrealworking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Answer_Lookup",
                columns: table => new
                {
                    Answer_Text = table.Column<string>(maxLength: 50, nullable: false),
                    Answer_Full_Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer_Lookup", x => x.Answer_Text);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ChangePassword = table.Column<bool>(nullable: false),
                    IsSuperUser = table.Column<bool>(nullable: false),
                    PasswordResetRequired = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 150, nullable: true),
                    LastName = table.Column<string>(maxLength: 150, nullable: true),
                    Guid_Id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sector",
                columns: table => new
                {
                    SectorId = table.Column<int>(nullable: false),
                    SectorName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sector", x => x.SectorId);
                });

            migrationBuilder.CreateTable(
                name: "Configuration",
                schema: "public",
                columns: table => new
                {
                    ConfigurationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConfigurationKey = table.Column<string>(nullable: true),
                    ConfigurationValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuration", x => x.ConfigurationId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasswordHistory",
                schema: "public",
                columns: table => new
                {
                    PasswordHistoryId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AspNetUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordHistory", x => x.PasswordHistoryId);
                    table.ForeignKey(
                        name: "FK_PasswordHistory_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sector_Industries",
                columns: table => new
                {
                    SectorId = table.Column<int>(nullable: false),
                    IndustryId = table.Column<int>(nullable: false),
                    IndustryName = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sector_Industries", x => new { x.SectorId, x.IndustryId });
                    table.ForeignKey(
                        name: "FK_Sector_Industries_Sector_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sector",
                        principalColumn: "SectorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticDemographics",
                columns: table => new
                {
                    AnalyticDemographicId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SectorId = table.Column<int>(nullable: false),
                    IndustryId = table.Column<int>(nullable: false),
                    Assets = table.Column<string>(maxLength: 100, nullable: true),
                    Size = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticDemographics", x => x.AnalyticDemographicId);
                    table.ForeignKey(
                        name: "FK_AnalyticDemographics_Sector_Industries_SectorId_IndustryId",
                        columns: x => new { x.SectorId, x.IndustryId },
                        principalTable: "Sector_Industries",
                        principalColumns: new[] { "SectorId", "IndustryId" },
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                schema: "public",
                columns: table => new
                {
                    Assessment_Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssessmentCreatedDate = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    AssessmentCreatorId = table.Column<string>(nullable: true),
                    LastAccessedDate = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    Alias = table.Column<string>(maxLength: 50, nullable: true),
                    Assessment_GUID = table.Column<string>(nullable: true),
                    Assessment_Date = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    AnalyticDemographicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Assessment_Id);
                    table.ForeignKey(
                        name: "FK_Assessments_AnalyticDemographics_AnalyticDemographicId",
                        column: x => x.AnalyticDemographicId,
                        principalTable: "AnalyticDemographics",
                        principalColumn: "AnalyticDemographicId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Assessments_AspNetUsers_AssessmentCreatorId",
                        column: x => x.AssessmentCreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticQuestionAnswer",
                schema: "public",
                columns: table => new
                {
                    AnalyticQuestionId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Assessment_Id = table.Column<int>(nullable: false),
                    Question_Or_Requirement_Id = table.Column<int>(nullable: false),
                    Answer_Text = table.Column<string>(maxLength: 50, nullable: false),
                    QuestionText = table.Column<string>(nullable: false),
                    Component_Guid = table.Column<Guid>(nullable: true),
                    Custom_Question_Guid = table.Column<string>(maxLength: 50, nullable: true),
                    Is_Requirement = table.Column<bool>(nullable: false),
                    Is_Component = table.Column<bool>(nullable: false),
                    Is_Framework = table.Column<bool>(nullable: false),
                    Assessment_Id1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticQuestionAnswer", x => x.AnalyticQuestionId);
                    table.ForeignKey(
                        name: "FK_AnalyticQuestionAnswer_Answer_Lookup_Answer_Text",
                        column: x => x.Answer_Text,
                        principalTable: "Answer_Lookup",
                        principalColumn: "Answer_Text",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AnalyticQuestionAnswer_Assessments_Assessment_Id",
                        column: x => x.Assessment_Id,
                        principalSchema: "public",
                        principalTable: "Assessments",
                        principalColumn: "Assessment_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalyticQuestionAnswer_Assessments_Assessment_Id1",
                        column: x => x.Assessment_Id1,
                        principalSchema: "public",
                        principalTable: "Assessments",
                        principalColumn: "Assessment_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Answer_Lookup",
                columns: new[] { "Answer_Text", "Answer_Full_Name" },
                values: new object[,]
                {
                    { "A", "Alternate" },
                    { "N", "No" },
                    { "NA", "Not Applicable" },
                    { "U", "Unanswered" },
                    { "Y", "Yes" }
                });

            migrationBuilder.InsertData(
                table: "Sector",
                columns: new[] { "SectorId", "SectorName" },
                values: new object[,]
                {
                    { 14, "Nuclear Reactors, Materials, and Waste Sector" },
                    { 13, "Information Technology Sector" },
                    { 12, "Healthcare and Public Health Sector" },
                    { 11, "Government Facilities Sector" },
                    { 10, "Food and Agriculture Sector" },
                    { 9, "Financial Services Sector" },
                    { 8, "Energy Sector" },
                    { 6, "Defense Industrial Base Sector" },
                    { 15, "Transportation Systems Sector" },
                    { 5, "Dams Sector" },
                    { 4, "Critical Manufacturing Sector" },
                    { 3, "Communications Sector" },
                    { 2, "Commercial Facilities Sector" },
                    { 1, "Chemical Sector (Not Oil and Gas)" },
                    { 7, "Emergency Services Sector" },
                    { 16, "Water and Wastewater Systems Sector" }
                });

            migrationBuilder.InsertData(
                table: "Sector_Industries",
                columns: new[] { "SectorId", "IndustryId", "IndustryName" },
                values: new object[,]
                {
                    { 1, 1, "Other" },
                    { 12, 61, "Other" },
                    { 12, 60, "Hospitals" },
                    { 11, 104, "Non-Public Facilities" },
                    { 11, 103, "Public Facilities" },
                    { 11, 59, "Tribal Governments" },
                    { 11, 58, "Territorial Governments" },
                    { 11, 57, "State Governments" },
                    { 11, 56, "Other" },
                    { 11, 55, "Local Governments" },
                    { 10, 102, "Supporting Facilities" },
                    { 10, 101, "Product Distribution" },
                    { 10, 100, "Product Transportation" },
                    { 12, 62, "Residential Care Facilities" },
                    { 10, 99, "Product Storage" },
                    { 10, 97, "Supply" },
                    { 10, 54, "Other" },
                    { 10, 53, "Food Services" },
                    { 10, 52, "Food Manufacturing Plants" },
                    { 10, 51, "Beverage Manufacturing Plants" },
                    { 9, 96, "Risk Transfer Products" },
                    { 9, 95, "Investment Products" },
                    { 9, 94, "Credit and Liquidity Products" },
                    { 9, 93, "Consumer Services" },
                    { 9, 50, "US Credit Unions" },
                    { 9, 49, "US Banks" },
                    { 9, 48, "Other" },
                    { 10, 98, "Processing, Packaging, and Production" },
                    { 8, 92, "Oil and Natural Gas" },
                    { 12, 105, "Direct Patient Care" },
                    { 12, 107, "Health Plans and Payers" },
                    { 16, 75, "Other" },
                    { 15, 74, "Pipelines (carries natural gas, hazardous liquids, and various chemicals.)" },
                    { 15, 73, "Other" },
                    { 15, 72, "Municipalities with Traffic Control Systems" },
                    { 15, 71, "Mass Transit and Passenger Rail" },
                    { 15, 70, "Maritime" },
                    { 15, 69, "Highway (truck transportation)" },
                    { 15, 68, "Freight Rail" },
                    { 15, 67, "Aviation" },
                    { 14, 120, "Radioactive Materials" },
                    { 14, 119, "Radioactive Waste" },
                    { 14, 118, "Nuclear Materials Transport" },
                    { 12, 106, "Health IT" },
                    { 14, 117, "Fuel Cycle Facilities" },
                    { 14, 65, "Operating Nuclear Power Plants" },
                    { 13, 116, "Incident Management" },
                    { 13, 115, "Internet Routing and Connection" },
                    { 13, 114, "Internet Content and Service Providers" },
                    { 13, 113, "Identity and Trust Support Management" },
                    { 13, 112, "DNS Services" },
                    { 13, 111, "IT Production" },
                    { 13, 64, "Other" },
                    { 13, 63, "Information Technology" },
                    { 12, 110, "Support Services" },
                    { 12, 109, "Medical Materials" },
                    { 12, 108, "Fatality Management Services" },
                    { 14, 66, "Other" },
                    { 8, 47, "Petroleum Refineries" },
                    { 8, 46, "Other" },
                    { 8, 45, "Natural Gas      " },
                    { 4, 18, "Transportation and Heavy Equipment Manufacturing" },
                    { 4, 17, "Primary Metal Manufacturing" },
                    { 4, 16, "Other" },
                    { 4, 15, "Machinery Manufacturing" },
                    { 4, 14, "Electrical Equipment, Appliance and Component Manufacturing" },
                    { 3, 86, "Wireline" },
                    { 3, 85, "Satellite" },
                    { 3, 84, "Cable" },
                    { 3, 83, "Broadcasting" },
                    { 3, 13, "Wireless Communications Service Providers" },
                    { 3, 12, "Telecommunications" },
                    { 3, 11, "Other" },
                    { 4, 87, "Manufacturing" },
                    { 2, 10, "Sports Leagues" },
                    { 2, 8, "Real Estate" },
                    { 2, 7, "Public Assembly" },
                    { 2, 6, "Outdoor Events" },
                    { 2, 5, "Other" },
                    { 2, 4, "Lodging" },
                    { 2, 3, "Gaming" },
                    { 2, 2, "Entertainment and Media" },
                    { 1, 82, "Agricultural Products" },
                    { 1, 81, "Consumer Products" },
                    { 1, 80, "Pharmaceutical Products" },
                    { 1, 79, "Specialty Products" },
                    { 1, 78, "Basic Chemicals" },
                    { 2, 9, "Retail" },
                    { 4, 88, "Heavy Machinery Manufacturing" },
                    { 5, 19, "Dams" },
                    { 5, 20, "Other" },
                    { 8, 44, "Electric Power Generation, Transmission and Distribution      " },
                    { 7, 43, "Public Works" },
                    { 7, 42, "Other" },
                    { 7, 41, "Law Enforcement    " },
                    { 7, 40, "Fire and Rescue Services" },
                    { 7, 39, "Emergency Medical Services" },
                    { 7, 38, "Emergency Management" },
                    { 6, 37, "Weapons" },
                    { 6, 36, "Troop Support" },
                    { 6, 35, "Structural Industry Commodities" },
                    { 6, 34, "Space" },
                    { 6, 33, "Shipbuilding Industry" },
                    { 6, 32, "Research and Development Facilities" },
                    { 6, 31, "Other" },
                    { 6, 30, "Missile Industry" },
                    { 6, 29, "Mechanical Industry Commodities" },
                    { 6, 28, "Electronics" },
                    { 6, 27, "Electrical Industry Commodities" },
                    { 6, 26, "Defense Contractors    " },
                    { 6, 25, "Communications" },
                    { 6, 24, "Combat Vehicle" },
                    { 6, 23, "Ammunition" },
                    { 6, 22, "Aircraft Industry" },
                    { 5, 91, "Tailings and Waste Impoundments" },
                    { 5, 90, "Navigation Locks" },
                    { 5, 89, "Levees" },
                    { 5, 21, "Private Hydropower Facilities in the US" },
                    { 16, 76, "Public Water Systems" },
                    { 16, 77, "Publicly Owned Treatment Works" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticDemographics_SectorId_IndustryId",
                table: "AnalyticDemographics",
                columns: new[] { "SectorId", "IndustryId" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticQuestionAnswer_Answer_Text",
                schema: "public",
                table: "AnalyticQuestionAnswer",
                column: "Answer_Text");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticQuestionAnswer_Assessment_Id",
                schema: "public",
                table: "AnalyticQuestionAnswer",
                column: "Assessment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticQuestionAnswer_Assessment_Id1",
                schema: "public",
                table: "AnalyticQuestionAnswer",
                column: "Assessment_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_AnalyticDemographicId",
                schema: "public",
                table: "Assessments",
                column: "AnalyticDemographicId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_AssessmentCreatorId",
                schema: "public",
                table: "Assessments",
                column: "AssessmentCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordHistory_CreatedUserId",
                schema: "public",
                table: "PasswordHistory",
                column: "CreatedUserId");
        }

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
                name: "AnalyticQuestionAnswer",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Configuration",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PasswordHistory",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Answer_Lookup");

            migrationBuilder.DropTable(
                name: "Assessments",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AnalyticDemographics");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Sector_Industries");

            migrationBuilder.DropTable(
                name: "Sector");
        }
    }
}

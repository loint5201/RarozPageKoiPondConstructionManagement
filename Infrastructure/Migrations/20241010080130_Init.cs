using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KoiDesigns",
                columns: table => new
                {
                    DesignID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CostEstimate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KoiDesig__32B8E17F671C8093", x => x.DesignID);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceServices",
                columns: table => new
                {
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Maintena__C51BB0EAF9178D2D", x => x.ServiceID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPolicies",
                columns: table => new
                {
                    PolicyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentP__2E13394406BD6B45", x => x.PolicyID);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    PromotionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Promotio__52C42F2F325EA4D0", x => x.PromotionID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__8AFACE3AC9B5849C", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCAC0619AF3F", x => x.UserID);
                    table.ForeignKey(
                        name: "FK__Users__RoleID__3A81B327",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "ConstructionRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    DesignID = table.Column<int>(type: "int", nullable: true),
                    CustomDesignDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Construc__33A8519A9A183ECA", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK__Construct__Custo__3F466844",
                        column: x => x.CustomerID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__Construct__Desig__403A8C7D",
                        column: x => x.DesignID,
                        principalTable: "KoiDesigns",
                        principalColumn: "DesignID");
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequests",
                columns: table => new
                {
                    ServiceRequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    ServiceID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ServiceR__790F6CAB08527AA3", x => x.ServiceRequestID);
                    table.ForeignKey(
                        name: "FK__ServiceRe__Custo__4CA06362",
                        column: x => x.CustomerID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__ServiceRe__Servi__4D94879B",
                        column: x => x.ServiceID,
                        principalTable: "MaintenanceServices",
                        principalColumn: "ServiceID");
                });

            migrationBuilder.CreateTable(
                name: "ConstructionProcess",
                columns: table => new
                {
                    ProcessID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(type: "int", nullable: true),
                    Step = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AssignedStaffID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Construc__1B39A976DC28FE0E", x => x.ProcessID);
                    table.ForeignKey(
                        name: "FK__Construct__Assig__45F365D3",
                        column: x => x.AssignedStaffID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__Construct__Reque__44FF419A",
                        column: x => x.RequestID,
                        principalTable: "ConstructionRequests",
                        principalColumn: "RequestID");
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrderHistory",
                columns: table => new
                {
                    HistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    RequestID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__4D7B4ADD623FA230", x => x.HistoryID);
                    table.ForeignKey(
                        name: "FK__CustomerO__Custo__5CD6CB2B",
                        column: x => x.CustomerID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__CustomerO__Reque__5DCAEF64",
                        column: x => x.RequestID,
                        principalTable: "ConstructionRequests",
                        principalColumn: "RequestID");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    RequestID = table.Column<int>(type: "int", nullable: true),
                    ServiceRequestID = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Feedback__6A4BEDF672535547", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK__Feedback__Custom__5441852A",
                        column: x => x.CustomerID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__Feedback__Reques__5535A963",
                        column: x => x.RequestID,
                        principalTable: "ConstructionRequests",
                        principalColumn: "RequestID");
                    table.ForeignKey(
                        name: "FK__Feedback__Servic__5629CD9C",
                        column: x => x.ServiceRequestID,
                        principalTable: "ServiceRequests",
                        principalColumn: "ServiceRequestID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionProcess_AssignedStaffID",
                table: "ConstructionProcess",
                column: "AssignedStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionProcess_RequestID",
                table: "ConstructionProcess",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionRequests_CustomerID",
                table: "ConstructionRequests",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionRequests_DesignID",
                table: "ConstructionRequests",
                column: "DesignID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderHistory_CustomerID",
                table: "CustomerOrderHistory",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderHistory_RequestID",
                table: "CustomerOrderHistory",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_CustomerID",
                table: "Feedback",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_RequestID",
                table: "Feedback",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_ServiceRequestID",
                table: "Feedback",
                column: "ServiceRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_CustomerID",
                table: "ServiceRequests",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_ServiceID",
                table: "ServiceRequests",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D105349EE3CA92",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstructionProcess");

            migrationBuilder.DropTable(
                name: "CustomerOrderHistory");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "PaymentPolicies");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "ConstructionRequests");

            migrationBuilder.DropTable(
                name: "ServiceRequests");

            migrationBuilder.DropTable(
                name: "KoiDesigns");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MaintenanceServices");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitV16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Feedback__Servic__5629CD9C",
                table: "Feedback");

            migrationBuilder.DropTable(
                name: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_ServiceRequestID",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "ServiceRequestID",
                table: "Feedback");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceRequestID",
                table: "Feedback",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServiceRequests",
                columns: table => new
                {
                    ServiceRequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    ServiceID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.AddForeignKey(
                name: "FK__Feedback__Servic__5629CD9C",
                table: "Feedback",
                column: "ServiceRequestID",
                principalTable: "ServiceRequests",
                principalColumn: "ServiceRequestID");
        }
    }
}

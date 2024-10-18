using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitV13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "ConstructionRequests");

            migrationBuilder.AddColumn<int>(
                name: "DesignType",
                table: "KoiDesigns",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceServiceId",
                table: "ConstructionRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionRequests_MaintenanceServiceId",
                table: "ConstructionRequests",
                column: "MaintenanceServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionRequests_MaintenanceServices_MaintenanceServiceId",
                table: "ConstructionRequests",
                column: "MaintenanceServiceId",
                principalTable: "MaintenanceServices",
                principalColumn: "ServiceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionRequests_MaintenanceServices_MaintenanceServiceId",
                table: "ConstructionRequests");

            migrationBuilder.DropIndex(
                name: "IX_ConstructionRequests_MaintenanceServiceId",
                table: "ConstructionRequests");

            migrationBuilder.DropColumn(
                name: "DesignType",
                table: "KoiDesigns");

            migrationBuilder.DropColumn(
                name: "MaintenanceServiceId",
                table: "ConstructionRequests");

            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "ConstructionRequests",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

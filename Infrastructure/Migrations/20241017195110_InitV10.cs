using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitV10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "KoiDesigns",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "CustomerOrderHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "CustomerOrderHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "ConstructionRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "KoiDesigns");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "CustomerOrderHistory");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "CustomerOrderHistory");

            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "ConstructionRequests");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace coreHotelRoomBookingAdminPortal.Migrations
{
    public partial class Payment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentCardNumber",
                table: "Payments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StripePaymentID",
                table: "Payments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentCardNumber",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "StripePaymentID",
                table: "Payments");
        }
    }
}

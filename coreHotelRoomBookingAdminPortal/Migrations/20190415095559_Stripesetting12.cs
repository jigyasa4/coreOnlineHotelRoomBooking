using Microsoft.EntityFrameworkCore.Migrations;

namespace coreHotelRoomBookingAdminPortal.Migrations
{
    public partial class Stripesetting12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StripeSettings",
                columns: table => new
                {
                    SecretKey = table.Column<string>(nullable: false),
                    PublishableKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeSettings", x => x.SecretKey);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StripeSettings");
        }
    }
}

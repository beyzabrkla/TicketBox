using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBox.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class fixEntityBooking3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketCount",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketCount",
                table: "Bookings");
        }
    }
}

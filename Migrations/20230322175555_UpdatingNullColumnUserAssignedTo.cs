using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketingSystemIT.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingNullColumnUserAssignedTo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_AssignedEmployeeID",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "AssignedEmployeeID",
                table: "Tickets",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_AssignedEmployeeID",
                table: "Tickets",
                column: "AssignedEmployeeID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_AssignedEmployeeID",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "AssignedEmployeeID",
                table: "Tickets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_AssignedEmployeeID",
                table: "Tickets",
                column: "AssignedEmployeeID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing.Migrations
{
    /// <inheritdoc />
    public partial class TicketTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "Created_At");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Events",
                newName: "time");

            migrationBuilder.RenameColumn(
                name: "MaxAudience",
                table: "Events",
                newName: "Max_Audience");

            migrationBuilder.RenameColumn(
                name: "IsPublished",
                table: "Events",
                newName: "Is_Published");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Events",
                newName: "Created_At");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Unique_Number = table.Column<string>(type: "varchar(10)", nullable: false),
                    Valid_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Event_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_EventId",
                table: "Ticket",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserId",
                table: "Ticket",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.RenameColumn(
                name: "Created_At",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "time",
                table: "Events",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "Max_Audience",
                table: "Events",
                newName: "MaxAudience");

            migrationBuilder.RenameColumn(
                name: "Is_Published",
                table: "Events",
                newName: "IsPublished");

            migrationBuilder.RenameColumn(
                name: "Created_At",
                table: "Events",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");
        }
    }
}

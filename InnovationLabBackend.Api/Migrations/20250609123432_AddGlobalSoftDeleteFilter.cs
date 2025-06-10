using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnovationLabBackend.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddGlobalSoftDeleteFilter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "EventAgendas");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EventAgendas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "EventAgendas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EventAgendas",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

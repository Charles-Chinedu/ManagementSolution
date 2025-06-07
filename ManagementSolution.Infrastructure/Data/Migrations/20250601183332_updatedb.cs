using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSolution.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CivilId",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "CivilId",
                table: "Sanctions");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "Sanctions");

            migrationBuilder.DropColumn(
                name: "CivilId",
                table: "Overtimes");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "Overtimes");

            migrationBuilder.DropColumn(
                name: "CivilId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "FileNumber",
                table: "Vacations",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "FileNumber",
                table: "Sanctions",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "FileNumber",
                table: "Overtimes",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "FileNumber",
                table: "Doctors",
                newName: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Vacations",
                newName: "FileNumber");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Sanctions",
                newName: "FileNumber");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Overtimes",
                newName: "FileNumber");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Doctors",
                newName: "FileNumber");

            migrationBuilder.AddColumn<string>(
                name: "CivilId",
                table: "Vacations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "Vacations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CivilId",
                table: "Sanctions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "Sanctions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CivilId",
                table: "Overtimes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "Overtimes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CivilId",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

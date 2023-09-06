using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharpToDo.Repositories.Ef.Migrations
{
    /// <inheritdoc />
    public partial class AddReminderProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Reminders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Reminders");
        }
    }
}

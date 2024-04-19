using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trello_services.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnIntoCardTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDueDayComplete",
                table: "card",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDueDayComplete",
                table: "card");
        }
    }
}

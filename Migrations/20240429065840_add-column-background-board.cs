using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trello_services.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnbackgroundboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "background",
                table: "board",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "background",
                table: "board");
        }
    }
}

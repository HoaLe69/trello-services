using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trello_services.Migrations
{
    /// <inheritdoc />
    public partial class boardRemoveField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "star",
                table: "board");

            migrationBuilder.AlterColumn<int>(
                name: "role",
                table: "user_worksapce",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "star",
                table: "user_board",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "star",
                table: "user_board");

            migrationBuilder.AlterColumn<int>(
                name: "role",
                table: "user_worksapce",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "star",
                table: "board",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

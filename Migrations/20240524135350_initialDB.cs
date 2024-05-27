using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trello_services.Migrations
{
    /// <inheritdoc />
    public partial class initialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    displayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    avatar_path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    verify_email = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "workspace",
                columns: table => new
                {
                    workSpaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    theme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workspace", x => x.workSpaceId);
                });

            migrationBuilder.CreateTable(
                name: "board",
                columns: table => new
                {
                    boardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    workSpaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    background = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    orderColumnIds = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_board", x => x.boardId);
                    table.ForeignKey(
                        name: "FK_board_workspace_workSpaceId",
                        column: x => x.workSpaceId,
                        principalTable: "workspace",
                        principalColumn: "workSpaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_worksapce",
                columns: table => new
                {
                    workSpaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_worksapce", x => new { x.userId, x.workSpaceId });
                    table.ForeignKey(
                        name: "FK_user_worksapce_user_userId",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_worksapce_workspace_workSpaceId",
                        column: x => x.workSpaceId,
                        principalTable: "workspace",
                        principalColumn: "workSpaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "column",
                columns: table => new
                {
                    columnId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    orderCardIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    boardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_column", x => x.columnId);
                    table.ForeignKey(
                        name: "FK_column_board_boardId",
                        column: x => x.boardId,
                        principalTable: "board",
                        principalColumn: "boardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "label",
                columns: table => new
                {
                    labelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    labelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    theme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    boardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_label", x => x.labelId);
                    table.ForeignKey(
                        name: "FK_label_board_boardId",
                        column: x => x.boardId,
                        principalTable: "board",
                        principalColumn: "boardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_board",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    boardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    star = table.Column<bool>(type: "bit", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_board", x => new { x.userId, x.boardId });
                    table.ForeignKey(
                        name: "FK_user_board_board_boardId",
                        column: x => x.boardId,
                        principalTable: "board",
                        principalColumn: "boardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_board_user_userId",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "card",
                columns: table => new
                {
                    cardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDueDayComplete = table.Column<bool>(type: "bit", nullable: true),
                    columnId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card", x => x.cardId);
                    table.ForeignKey(
                        name: "FK_card_column_columnId",
                        column: x => x.columnId,
                        principalTable: "column",
                        principalColumn: "columnId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "activity",
                columns: table => new
                {
                    activityId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activity", x => x.activityId);
                    table.ForeignKey(
                        name: "FK_activity_card_cardId",
                        column: x => x.cardId,
                        principalTable: "card",
                        principalColumn: "cardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_activity_user_userId",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "card_label",
                columns: table => new
                {
                    cardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    labelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card_label", x => new { x.cardId, x.labelId });
                    table.ForeignKey(
                        name: "FK_card_label_card_cardId",
                        column: x => x.cardId,
                        principalTable: "card",
                        principalColumn: "cardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_card_label_label_labelId",
                        column: x => x.labelId,
                        principalTable: "label",
                        principalColumn: "labelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checklist",
                columns: table => new
                {
                    checkListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checklist", x => x.checkListId);
                    table.ForeignKey(
                        name: "FK_checklist_card_cardId",
                        column: x => x.cardId,
                        principalTable: "card",
                        principalColumn: "cardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    commentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment", x => x.commentId);
                    table.ForeignKey(
                        name: "FK_comment_card_cardId",
                        column: x => x.cardId,
                        principalTable: "card",
                        principalColumn: "cardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comment_user_userId",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_card",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_card", x => new { x.cardId, x.userId });
                    table.ForeignKey(
                        name: "FK_user_card_card_cardId",
                        column: x => x.cardId,
                        principalTable: "card",
                        principalColumn: "cardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_card_user_userId",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "check_list_detail",
                columns: table => new
                {
                    clDetailId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    checkListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_check_list_detail", x => x.clDetailId);
                    table.ForeignKey(
                        name: "FK_check_list_detail_checklist_checkListId",
                        column: x => x.checkListId,
                        principalTable: "checklist",
                        principalColumn: "checkListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_activity_cardId",
                table: "activity",
                column: "cardId");

            migrationBuilder.CreateIndex(
                name: "IX_activity_userId",
                table: "activity",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_board_workSpaceId",
                table: "board",
                column: "workSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_card_columnId",
                table: "card",
                column: "columnId");

            migrationBuilder.CreateIndex(
                name: "IX_card_label_labelId",
                table: "card_label",
                column: "labelId");

            migrationBuilder.CreateIndex(
                name: "IX_check_list_detail_checkListId",
                table: "check_list_detail",
                column: "checkListId");

            migrationBuilder.CreateIndex(
                name: "IX_checklist_cardId",
                table: "checklist",
                column: "cardId");

            migrationBuilder.CreateIndex(
                name: "IX_column_boardId",
                table: "column",
                column: "boardId");

            migrationBuilder.CreateIndex(
                name: "IX_comment_cardId",
                table: "comment",
                column: "cardId");

            migrationBuilder.CreateIndex(
                name: "IX_comment_userId",
                table: "comment",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_label_boardId",
                table: "label",
                column: "boardId");

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_board_boardId",
                table: "user_board",
                column: "boardId");

            migrationBuilder.CreateIndex(
                name: "IX_user_card_userId",
                table: "user_card",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_user_worksapce_workSpaceId",
                table: "user_worksapce",
                column: "workSpaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activity");

            migrationBuilder.DropTable(
                name: "card_label");

            migrationBuilder.DropTable(
                name: "check_list_detail");

            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "user_board");

            migrationBuilder.DropTable(
                name: "user_card");

            migrationBuilder.DropTable(
                name: "user_worksapce");

            migrationBuilder.DropTable(
                name: "label");

            migrationBuilder.DropTable(
                name: "checklist");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "card");

            migrationBuilder.DropTable(
                name: "column");

            migrationBuilder.DropTable(
                name: "board");

            migrationBuilder.DropTable(
                name: "workspace");
        }
    }
}

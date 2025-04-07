using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NovaTasks.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "labels",
                columns: table => new
                {
                    label_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    color = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_labels", x => x.label_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "workspaces",
                columns: table => new
                {
                    workspace_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    visibility = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workspaces", x => x.workspace_id);
                });

            migrationBuilder.CreateTable(
                name: "boards",
                columns: table => new
                {
                    board_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    workspace_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_boards", x => x.board_id);
                    table.ForeignKey(
                        name: "fk_boards_workspaces",
                        column: x => x.workspace_id,
                        principalTable: "workspaces",
                        principalColumn: "workspace_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workspace_members",
                columns: table => new
                {
                    workspace_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workspace_members", x => new { x.workspace_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_workspace_members_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_workspace_members_workspaces",
                        column: x => x.workspace_id,
                        principalTable: "workspaces",
                        principalColumn: "workspace_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tasks_list",
                columns: table => new
                {
                    tasks_list_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    board_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks_list", x => x.tasks_list_id);
                    table.ForeignKey(
                        name: "fk_tasks_list_boards",
                        column: x => x.board_id,
                        principalTable: "boards",
                        principalColumn: "board_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    task_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tasks_list_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    is_complete = table.Column<bool>(type: "boolean", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    color = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.task_id);
                    table.ForeignKey(
                        name: "fk_tasks_tasks_list",
                        column: x => x.tasks_list_id,
                        principalTable: "tasks_list",
                        principalColumn: "tasks_list_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "task_assignees",
                columns: table => new
                {
                    task_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_assignees", x => new { x.task_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_task_assignees_tasks_task_id",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "task_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_task_assignees_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "task_labels",
                columns: table => new
                {
                    task_id = table.Column<int>(type: "integer", nullable: false),
                    label_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_labels", x => new { x.task_id, x.label_id });
                    table.ForeignKey(
                        name: "fk_task_labels_labels",
                        column: x => x.label_id,
                        principalTable: "labels",
                        principalColumn: "label_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_task_labels_tasks",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "task_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_boards_workspace_id",
                table: "boards",
                column: "workspace_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_assignees_user_id",
                table: "task_assignees",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_labels_label_id",
                table: "task_labels",
                column: "label_id");

            migrationBuilder.CreateIndex(
                name: "ix_tasks_due_date",
                table: "tasks",
                column: "due_date");

            migrationBuilder.CreateIndex(
                name: "ix_tasks_list_id",
                table: "tasks",
                column: "tasks_list_id");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_list_board_id",
                table: "tasks_list",
                column: "board_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_workspace_members_user_id",
                table: "workspace_members",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "task_assignees");

            migrationBuilder.DropTable(
                name: "task_labels");

            migrationBuilder.DropTable(
                name: "workspace_members");

            migrationBuilder.DropTable(
                name: "labels");

            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "tasks_list");

            migrationBuilder.DropTable(
                name: "boards");

            migrationBuilder.DropTable(
                name: "workspaces");
        }
    }
}

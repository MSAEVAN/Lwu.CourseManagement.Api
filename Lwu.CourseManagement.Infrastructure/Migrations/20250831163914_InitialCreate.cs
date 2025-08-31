using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lwu.CourseManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    Username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UserPrincipal = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: true),
                    StaffId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedByUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_AppUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUsers_AppUsers_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppUsers_AppUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedByUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staffs_AppUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staffs_AppUsers_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Staffs_AppUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedByUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_AppUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_AppUsers_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Students_AppUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreatedByUserId", "CreatedOn", "DeletedByUserId", "DeletedOn", "Email", "FullName", "IsDeleted", "ModifiedByUserId", "ModifiedOn", "PasswordHash", "Role", "StaffId", "StudentId", "UserPrincipal", "Username" },
                values: new object[] { new Guid("eaa6081c-16f1-41be-9153-5662bc03e9fc"), new Guid("eaa6081c-16f1-41be-9153-5662bc03e9fc"), new DateTime(2025, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "admin@mail.com", "MSA Evan", false, null, null, "$2a$11$9.MV93UKWiuZtTmqzHmb2.ENRvyEHTB726NQgquEC77X4FGWTVuIq", "Stuff", null, null, "admin", "LwuAdmin" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_CreatedByUserId",
                table: "AppUsers",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_DeletedByUserId",
                table: "AppUsers",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_ModifiedByUserId",
                table: "AppUsers",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_StaffId",
                table: "AppUsers",
                column: "StaffId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_StudentId",
                table: "AppUsers",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_Username",
                table: "AppUsers",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_CreatedByUserId",
                table: "Staffs",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_DeletedByUserId",
                table: "Staffs",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_ModifiedByUserId",
                table: "Staffs",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CreatedByUserId",
                table: "Students",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DeletedByUserId",
                table: "Students",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ModifiedByUserId",
                table: "Students",
                column: "ModifiedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Staffs_StaffId",
                table: "AppUsers",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Students_StudentId",
                table: "AppUsers",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Staffs_StaffId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Students_StudentId",
                table: "AppUsers");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XuongCSharp.Migrations
{
    /// <inheritdoc />
    public partial class db3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "import_log",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerformedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImportDate = table.Column<long>(type: "bigint", nullable: false),
                    SuccessCount = table.Column<int>(type: "int", nullable: false),
                    FailCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_import_log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "staff_status_log",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OldStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    NewStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoggedDate = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff_status_log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_staff_status_log_staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "dbo",
                        principalTable: "staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "import_log_detail",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImportLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_import_log_detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_import_log_detail_import_log_ImportLogId",
                        column: x => x.ImportLogId,
                        principalSchema: "dbo",
                        principalTable: "import_log",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_import_log_detail_ImportLogId",
                schema: "dbo",
                table: "import_log_detail",
                column: "ImportLogId");

            migrationBuilder.CreateIndex(
                name: "IX_staff_status_log_StaffId",
                schema: "dbo",
                table: "staff_status_log",
                column: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "import_log_detail",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "staff_status_log",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "import_log",
                schema: "dbo");
        }
    }
}

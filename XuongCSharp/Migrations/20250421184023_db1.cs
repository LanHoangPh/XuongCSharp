using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XuongCSharp.Migrations
{
    /// <inheritdoc />
    public partial class db1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "department",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    CreatedDate = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDate = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "facility",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    CreatedDate = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDate = table.Column<long>(type: "bigint", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facility", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "major",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    CreatedDate = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDate = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_major", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "staff",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountFe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountFpt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    CreatedDate = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDate = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "department_facility",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdDepartment = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdFacility = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdStaff = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    CreatedDate = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDate = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department_facility", x => x.Id);
                    table.ForeignKey(
                        name: "FK_department_facility_department_IdDepartment",
                        column: x => x.IdDepartment,
                        principalSchema: "dbo",
                        principalTable: "department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_department_facility_facility_IdFacility",
                        column: x => x.IdFacility,
                        principalSchema: "dbo",
                        principalTable: "facility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_department_facility_staff_IdStaff",
                        column: x => x.IdStaff,
                        principalSchema: "dbo",
                        principalTable: "staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "major_facility",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdDepartmentFacility = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdMajor = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    CreatedDate = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDate = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_major_facility", x => x.Id);
                    table.ForeignKey(
                        name: "FK_major_facility_department_facility_IdDepartmentFacility",
                        column: x => x.IdDepartmentFacility,
                        principalSchema: "dbo",
                        principalTable: "department_facility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_major_facility_major_IdMajor",
                        column: x => x.IdMajor,
                        principalSchema: "dbo",
                        principalTable: "major",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "staff_major_facility",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdMajorFacility = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdStaff = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    CreatedDate = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDate = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff_major_facility", x => x.Id);
                    table.ForeignKey(
                        name: "FK_staff_major_facility_major_facility_IdMajorFacility",
                        column: x => x.IdMajorFacility,
                        principalSchema: "dbo",
                        principalTable: "major_facility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_staff_major_facility_staff_IdStaff",
                        column: x => x.IdStaff,
                        principalSchema: "dbo",
                        principalTable: "staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_department_facility_IdDepartment",
                schema: "dbo",
                table: "department_facility",
                column: "IdDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_department_facility_IdFacility",
                schema: "dbo",
                table: "department_facility",
                column: "IdFacility");

            migrationBuilder.CreateIndex(
                name: "IX_department_facility_IdStaff",
                schema: "dbo",
                table: "department_facility",
                column: "IdStaff");

            migrationBuilder.CreateIndex(
                name: "IX_major_facility_IdDepartmentFacility",
                schema: "dbo",
                table: "major_facility",
                column: "IdDepartmentFacility");

            migrationBuilder.CreateIndex(
                name: "IX_major_facility_IdMajor",
                schema: "dbo",
                table: "major_facility",
                column: "IdMajor");

            migrationBuilder.CreateIndex(
                name: "IX_staff_major_facility_IdMajorFacility",
                schema: "dbo",
                table: "staff_major_facility",
                column: "IdMajorFacility");

            migrationBuilder.CreateIndex(
                name: "IX_staff_major_facility_IdStaff",
                schema: "dbo",
                table: "staff_major_facility",
                column: "IdStaff");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "staff_major_facility",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "major_facility",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "department_facility",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "major",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "department",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "facility",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "staff",
                schema: "dbo");
        }
    }
}

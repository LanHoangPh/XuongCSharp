using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XuongCSharp.Migrations
{
    /// <inheritdoc />
    public partial class db2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "department",
                columns: new[] { "Id", "Code", "CreatedDate", "LastModifiedDate", "Name", "Status" },
                values: new object[,]
                {
                    { new Guid("550e8400-e29b-41d4-a716-446655440020"), "DEP001", 1627849200000L, 1627935600000L, "Department One", (byte)1 },
                    { new Guid("550e8400-e29b-41d4-a716-446655440021"), "DEP002", 1627849200000L, 1627935600000L, "Department Two", (byte)1 },
                    { new Guid("550e8400-e29b-41d4-a716-446655440022"), "DEP003", 1627849200000L, 1627935600000L, "Department Three", (byte)1 }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "facility",
                columns: new[] { "Id", "Code", "CreatedDate", "LastModifiedDate", "Name", "Status" },
                values: new object[,]
                {
                    { new Guid("550e8400-e29b-41d4-a716-446655440010"), "FAC001", 1627849200000L, 1627935600000L, "HN", (byte)1 },
                    { new Guid("550e8400-e29b-41d4-a716-446655440011"), "FAC002", 1627849200000L, 1627935600000L, "HCM", (byte)1 }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "major",
                columns: new[] { "Id", "Code", "CreatedDate", "LastModifiedDate", "Name", "Status" },
                values: new object[,]
                {
                    { new Guid("550e8400-e29b-41d4-a716-446655440040"), "MAJ001", 1627849200000L, 1627935600000L, "Major One", (byte)1 },
                    { new Guid("550e8400-e29b-41d4-a716-446655440041"), "MAJ002", 1627849200000L, 1627935600000L, "Major Two", (byte)1 },
                    { new Guid("550e8400-e29b-41d4-a716-446655440042"), "MAJ003", 1627849200000L, 1627935600000L, "Major Three", (byte)1 }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "staff",
                columns: new[] { "Id", "AccountFe", "AccountFpt", "CreatedDate", "LastModifiedDate", "Name", "StaffCode", "Status" },
                values: new object[,]
                {
                    { new Guid("550e8400-e29b-41d4-a716-446655440000"), "fe_account1@fe.edu.vn", "fpt_account1@fpt.edu.vn", 1627849200000L, 1627935600000L, "John wick", "ST001", (byte)1 },
                    { new Guid("550e8400-e29b-41d4-a716-446655440001"), "fe_account2@fe.edu.vn", "fpt_account2@fpt.edu.vn", 1627849200000L, 1627935600000L, "Top1 Flo", "ST002", (byte)1 },
                    { new Guid("550e8400-e29b-41d4-a716-446655440002"), "fe_account3@fe.edu.vn", "fpt_account3@fpt.edu.vn", 1627849200000L, 1627935600000L, "Lục Luật", "ST003", (byte)1 }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "department_facility",
                columns: new[] { "Id", "CreatedDate", "IdDepartment", "IdFacility", "IdStaff", "LastModifiedDate", "Status" },
                values: new object[,]
                {
                    { new Guid("550e8400-e29b-41d4-a716-446655440030"), 1627849200000L, new Guid("550e8400-e29b-41d4-a716-446655440020"), new Guid("550e8400-e29b-41d4-a716-446655440010"), new Guid("550e8400-e29b-41d4-a716-446655440000"), 1627935600000L, (byte)1 },
                    { new Guid("550e8400-e29b-41d4-a716-446655440031"), 1627849200000L, new Guid("550e8400-e29b-41d4-a716-446655440021"), new Guid("550e8400-e29b-41d4-a716-446655440011"), new Guid("550e8400-e29b-41d4-a716-446655440001"), 1627935600000L, (byte)1 },
                    { new Guid("550e8400-e29b-41d4-a716-446655440032"), 1627849200000L, new Guid("550e8400-e29b-41d4-a716-446655440022"), new Guid("550e8400-e29b-41d4-a716-446655440011"), new Guid("550e8400-e29b-41d4-a716-446655440002"), 1627935600000L, (byte)1 }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "major_facility",
                columns: new[] { "Id", "CreatedDate", "IdDepartmentFacility", "IdMajor", "LastModifiedDate", "Status" },
                values: new object[,]
                {
                    { new Guid("550e8400-e29b-41d4-a716-446655440050"), 1627849200000L, new Guid("550e8400-e29b-41d4-a716-446655440030"), new Guid("550e8400-e29b-41d4-a716-446655440040"), 1627935600000L, (byte)1 },
                    { new Guid("550e8400-e29b-41d4-a716-446655440051"), 1627849200000L, new Guid("550e8400-e29b-41d4-a716-446655440031"), new Guid("550e8400-e29b-41d4-a716-446655440041"), 1627935600000L, (byte)1 },
                    { new Guid("550e8400-e29b-41d4-a716-446655440052"), 1627849200000L, new Guid("550e8400-e29b-41d4-a716-446655440032"), new Guid("550e8400-e29b-41d4-a716-446655440042"), 1627935600000L, (byte)1 }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "staff_major_facility",
                columns: new[] { "Id", "CreatedDate", "IdMajorFacility", "IdStaff", "LastModifiedDate", "Status" },
                values: new object[,]
                {
                    { new Guid("550e8400-e29b-41d4-a716-446655440060"), 1627849200000L, new Guid("550e8400-e29b-41d4-a716-446655440050"), new Guid("550e8400-e29b-41d4-a716-446655440000"), 1627935600000L, (byte)1 },
                    { new Guid("550e8400-e29b-41d4-a716-446655440061"), 1627849200000L, new Guid("550e8400-e29b-41d4-a716-446655440051"), new Guid("550e8400-e29b-41d4-a716-446655440001"), 1627935600000L, (byte)1 },
                    { new Guid("550e8400-e29b-41d4-a716-446655440062"), 1627849200000L, new Guid("550e8400-e29b-41d4-a716-446655440052"), new Guid("550e8400-e29b-41d4-a716-446655440002"), 1627935600000L, (byte)1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "staff_major_facility",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440060"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "staff_major_facility",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440061"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "staff_major_facility",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440062"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "major_facility",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440050"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "major_facility",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440051"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "major_facility",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440052"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "department_facility",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440030"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "department_facility",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440031"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "department_facility",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440032"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "major",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440040"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "major",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440041"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "major",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440042"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "department",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440020"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "department",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440021"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "department",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440022"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "facility",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440010"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "facility",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440011"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "staff",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440000"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "staff",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440001"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "staff",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440002"));
        }
    }
}

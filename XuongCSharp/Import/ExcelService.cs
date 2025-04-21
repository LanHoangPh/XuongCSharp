using ClosedXML.Excel;
using System.Drawing;
using XuongCSharp.DTOs.ImportFile;

namespace XuongCSharp.Import
{
    public class ExcelService
    {
        public byte[] GenerateImportTemplate()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Employees");

            // Add headers
            worksheet.Cell(1, 1).Value = "Staff Code*";
            worksheet.Cell(1, 2).Value = "Full Name*";
            worksheet.Cell(1, 3).Value = "FPT Email*";
            worksheet.Cell(1, 4).Value = "FE Email*";
            worksheet.Cell(1, 5).Value = "Status* (1=Active, 0=Inactive)";
            worksheet.Cell(1, 6).Value = "Location Code*";
            worksheet.Cell(1, 7).Value = "Department Code*";
            worksheet.Cell(1, 8).Value = "Specialization Code*";

            // Add format information and sample data
            worksheet.Cell(2, 1).Value = "ABC123";
            worksheet.Cell(2, 2).Value = "Nguyen Van A";
            worksheet.Cell(2, 3).Value = "anv@fpt.edu.vn";
            worksheet.Cell(2, 4).Value = "anv@fe.edu.vn";
            worksheet.Cell(2, 5).Value = "1";
            worksheet.Cell(2, 6).Value = "HCM";
            worksheet.Cell(2, 7).Value = "IT";
            worksheet.Cell(2, 8).Value = "SE";

            // Format the headers
            var headerRange = worksheet.Range("A1:H1");
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

            // Adjust column widths
            worksheet.Columns().AdjustToContents();

            using var ms = new MemoryStream();
            workbook.SaveAs(ms);
            return ms.ToArray();
        }

        public List<StaffImportDto> ReadStaffFromExcel(Stream fileStream)
        {
            var staffList = new List<StaffImportDto>();

            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheet(1);

            // Skip header row
            var rows = worksheet.RowsUsed().Skip(1);

            foreach (var row in rows)
            {
                var staff = new StaffImportDto
                {
                    StaffCode = row.Cell(1).GetString().Trim(),
                    Name = row.Cell(2).GetString().Trim(),
                    AccountFpt = row.Cell(3).GetString().Trim(),
                    AccountFe = row.Cell(4).GetString().Trim(),
                    Status = byte.TryParse(row.Cell(5).GetString(), out byte status) ? status : (byte)1,
                    LocationCode = row.Cell(6).GetString().Trim(),
                    DepartmentCode = row.Cell(7).GetString().Trim(),
                    MajorCode = row.Cell(8).GetString().Trim()
                };

                staffList.Add(staff);
            }

            return staffList;
        }
    }
}

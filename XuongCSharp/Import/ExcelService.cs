using ClosedXML.Excel;
using System.Drawing;
using XuongCSharp.DTOs.ImportFile;

namespace XuongCSharp.Import
{
    public class ExcelService
    {
        public byte[] ExportStaffsToExcel(List<StaffDto> staffs)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Staffs");

            worksheet.Cell(1, 1).Value = "Name";
            worksheet.Cell(1, 2).Value = "Staff Code";
            worksheet.Cell(1, 3).Value = "FE Email";
            worksheet.Cell(1, 4).Value = "FPT Email";
            worksheet.Cell(1, 5).Value = "Status";
            worksheet.Cell(1, 6).Value = "Facility-Department-Major";
            worksheet.Cell(1, 7).Value = "Facility-Department-Major-Code";

            var headerRange = worksheet.Range("A1:G1");
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

            for (int i = 0; i < staffs.Count; i++)
            {
                var staff = staffs[i];
                var row = i + 2; 

                worksheet.Cell(row, 1).Value = staff.Name;
                worksheet.Cell(row, 2).Value = staff.StaffCode;
                worksheet.Cell(row, 3).Value = staff.AccountFe;
                worksheet.Cell(row, 4).Value = staff.AccountFpt;
                worksheet.Cell(row, 5).Value = staff.Status == 1 ? "Active" : "Inactive";
                var location = staff.Locations?.FirstOrDefault(l => l.StaffId == staff.Id);
                worksheet.Cell(row, 6).Value = location != null ? $"{location.FacilityName}-{location.DepartmentName}-{location.MajorName}" : "N/A";
                worksheet.Cell(row, 7).Value = location != null ? $"{location.FacilityCode}-{location.DepartmentCode}-{location.MajorCode}" : "N/A";
            }

            worksheet.Columns().AdjustToContents();

            using var ms = new MemoryStream();
            workbook.SaveAs(ms);
            return ms.ToArray();
        }
        public byte[] GenerateImportTemplate()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Employees");

            // Headers
            worksheet.Cell(1, 1).Value = "Staff Code*";
            worksheet.Cell(1, 2).Value = "Full Name*";
            worksheet.Cell(1, 3).Value = "FPT Email*";
            worksheet.Cell(1, 4).Value = "FE Email*";
            worksheet.Cell(1, 5).Value = "Status* (1=Active, 0=Inactive)";
            worksheet.Cell(1, 6).Value = "Facility-Department-Major*Name";
            worksheet.Cell(1, 7).Value = "Facility-Department-Major*Code";

            worksheet.Cell(2, 1).Value = "ABC123";
            worksheet.Cell(2, 2).Value = "Nguyen Van A";
            worksheet.Cell(2, 3).Value = "anv@fpt.edu.vn";
            worksheet.Cell(2, 4).Value = "anv@fe.edu.vn";
            worksheet.Cell(2, 5).Value = "1";
            worksheet.Cell(2, 6).Value = "HCM-IT-SE";
            worksheet.Cell(2, 7).Value = "HCM-IT-SE";

            var headerRange = worksheet.Range("A1:G1");
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

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

            var rows = worksheet.RowsUsed().Skip(1);

            foreach (var row in rows)
            {
                var staff = new StaffImportDto
                {
                    StaffCode = row.Cell(1).GetString()?.Trim() ?? string.Empty,
                    Name = row.Cell(2).GetString()?.Trim() ?? string.Empty,
                    AccountFpt = row.Cell(3).GetString()?.Trim() ?? string.Empty,
                    AccountFe = row.Cell(4).GetString()?.Trim() ?? string.Empty,
                    Status = byte.TryParse(row.Cell(5).GetString(), out byte status) ? status : (byte)1
                };

                //var locationCode = row.Cell(7).GetString()?.Trim() ?? string.Empty;
                //if (!string.IsNullOrEmpty(locationCode))
                //{
                //    var parts = locationCode.Split('-');
                //    if (parts.Length == 3)
                //    {
                //        staff.LocationCode = parts[0]; // FacilityCode
                //        staff.DepartmentCode = parts[1]; // DepartmentCode
                //        staff.MajorCode = parts[2]; // MajorCode
                //    }
                //    else
                //    {
                //        staff.LocationCode = string.Empty;
                //        staff.DepartmentCode = string.Empty;
                //        staff.MajorCode = string.Empty;
                //    }
                //}
                //else
                //{
                //    staff.LocationCode = string.Empty;
                //    staff.DepartmentCode = string.Empty;
                //    staff.MajorCode = string.Empty;
                //}

                staffList.Add(staff);
            }

            return staffList;
        }
    }
}

namespace XuongCSharp.DTOs.Staf
{
    public class CreateStaffDto
    {
        public string? StaffCode { get; set; }
        public string? Name { get; set; }
        public string? AccountFpt { get; set; }
        public string? AccountFe { get; set; }
        public byte Status { get; set; }
    }
}

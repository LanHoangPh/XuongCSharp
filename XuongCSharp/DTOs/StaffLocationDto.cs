namespace XuongCSharp.DTOs
{
    public class StaffLocationDto
    {
        public Guid FacilityId { get; set; }
        public string? FacilityName { get; set; }
        public string FacilityCode { get; set; }
        public Guid DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public Guid? MajorId { get; set; }
        public string? MajorName { get; set; }
        public string MajorCode { get; set; }
        public Guid StaffId { get; set; }
    }
}

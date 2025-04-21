namespace XuongCSharp.DTOs
{
    public class StaffLocationDto
    {
        public Guid FacilityId { get; set; }
        public string? FacilityName { get; set; }
        public Guid DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public Guid? MajorId { get; set; }
        public string? MajorName { get; set; }
    }
}

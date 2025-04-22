namespace XuongCSharp.DTOs.Staf
{
    public class StaffDto
    {
        public Guid Id { get; set; }
        public string? StaffCode { get; set; }
        public string? Name { get; set; }
        public string? AccountFpt { get; set; }
        public string? AccountFe { get; set; }
        public byte Status { get; set; }
        public long CreatedDate { get; set; }
        public long LastModifiedDate { get; set; }
        public List<StaffLocationDto> Locations { get; set; } = new();
    }
}

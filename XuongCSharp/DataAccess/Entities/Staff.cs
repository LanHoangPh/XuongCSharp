namespace XuongCSharp.DataAccess.Entities
{
    public class Staff
    {
        [Key]
        public Guid Id { get; set; }

        public string? AccountFe { get; set; }

        public string? AccountFpt { get; set; }

        public string? Name { get; set; }

        public string? StaffCode
        {
            get; set;
        }
        public byte? Status { get; set; }

        public long? CreatedDate { get; set; }

        public long? LastModifiedDate { get; set; }

        public virtual ICollection<DepartmentFacility> DepartmentFacilities { get; set; } = new List<DepartmentFacility>();

        public virtual ICollection<StaffMajorFacility> StaffMajorFacilities { get; set; } = new List<StaffMajorFacility>();
    }
}

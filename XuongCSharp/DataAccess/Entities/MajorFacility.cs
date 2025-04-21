namespace XuongCSharp.DataAccess.Entities
{
    public class MajorFacility
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? IdDepartmentFacility { get; set; }

        public Guid? IdMajor { get; set; }
        public byte? Status { get; set; }

        public long? CreatedDate { get; set; }

        public long? LastModifiedDate { get; set; }
       
        public virtual DepartmentFacility? DepartmentFacility { get; set; }

        public virtual Major? Major{ get; set; }

        public virtual ICollection<StaffMajorFacility> StaffMajorFacilities { get; set; } = new List<StaffMajorFacility>();
    }
}

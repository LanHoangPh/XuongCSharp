namespace XuongCSharp.DataAccess.Entities
{
    public class StaffMajorFacility
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? IdMajorFacility { get; set; }

        public Guid? IdStaff { get; set; }
        public byte? Status { get; set; }

        public long? CreatedDate { get; set; }

        public long? LastModifiedDate { get; set; }

        public virtual MajorFacility? MajorFacility { get; set; }

        public virtual Staff? Staff { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace XuongCSharp.DataAccess.Entities
{
    public class DepartmentFacility
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? IdDepartment { get; set; }
        public Guid? IdFacility { get; set; }
        public Guid? IdStaff { get; set; }
        public byte? Status { get; set; }
        public long? CreatedDate { get; set; }
        public long? LastModifiedDate { get; set; }
        public virtual Department? Department { get; set; }
        public virtual Facility? Facility { get; set; }
        public virtual Staff? Staff{ get; set; }
        public virtual ICollection<MajorFacility> MajorFacilities { get; set; } = new List<MajorFacility>();
    }
}

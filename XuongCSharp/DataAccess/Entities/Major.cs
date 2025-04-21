using System.ComponentModel.DataAnnotations.Schema;

namespace XuongCSharp.DataAccess.Entities
{
    public class Major
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public byte? Status { get; set; }
        public long? CreatedDate { get; set; }

        public long? LastModifiedDate { get; set; }
        public virtual ICollection<MajorFacility> MajorFacilities { get; set; } = new List<MajorFacility>();
    }
}

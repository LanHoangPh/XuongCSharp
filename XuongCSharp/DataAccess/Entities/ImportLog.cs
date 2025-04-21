using XuongCSharp.Import;

namespace XuongCSharp.DataAccess.Entities
{
    public class ImportLog
    {
        [Key]
        public Guid Id { get; set; }
        public string? PerformedBy { get; set; }
        public long ImportDate { get; set; }
        public int SuccessCount { get; set; }
        public int FailCount { get; set; }
        public virtual ICollection<ImportLogDetail> ImportLogDetails { get; set; } = new List<ImportLogDetail>();
    }
}

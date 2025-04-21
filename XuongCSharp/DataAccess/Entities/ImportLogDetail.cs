namespace XuongCSharp.DataAccess.Entities
{
    public class ImportLogDetail
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ImportLogId { get; set; }
        public string? RowData { get; set; }
        public string? ErrorMessage { get; set; }
        public virtual ImportLog? ImportLog { get; set; }
    }
}

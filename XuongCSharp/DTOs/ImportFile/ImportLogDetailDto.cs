namespace XuongCSharp.DTOs.ImportFile
{
    public class ImportLogDetailDto
    {
        public Guid Id { get; set; }
        public Guid ImportLogId { get; set; }
        public string? RowData { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

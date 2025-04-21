namespace XuongCSharp.DTOs.ImportFile
{
    public class ImportLogDto
    {
        public Guid Id { get; set; }
        public string? ImportedBy { get; set; }
        public long ImportDate { get; set; }
        public int SuccessCount { get; set; }
        public int FailedCount { get; set; }
    }
}

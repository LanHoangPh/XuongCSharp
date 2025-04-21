namespace XuongCSharp.DTOs.ImportFile
{
    public class ImportResultDto
    {
        public Guid ImportId { get; set; }
        public int SuccessCount { get; set; }
        public int FailCount { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}

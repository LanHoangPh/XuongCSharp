namespace XuongCSharp.DTOs.ImportFile
{
    public class ImportResultDto
    {
        public bool Success { get; set; }
        public Guid ImportId { get; set; }
        public int SuccessCount { get; set; }
        public int FailCount { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public string ErrorMessage { get; set; }
    }
}

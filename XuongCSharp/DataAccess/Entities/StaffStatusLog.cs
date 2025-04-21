namespace XuongCSharp.DataAccess.Entities
{
    public class StaffStatusLog
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StaffId { get; set; }
        public byte OldStatus { get; set; }
        public byte NewStatus { get; set; }
        public string? Reason { get; set; }
        public long LoggedDate { get; set; }
        public virtual Staff? Staff { get; set; }
    }
}

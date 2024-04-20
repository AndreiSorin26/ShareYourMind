namespace Services.DTOs.EntityMappings
{
    public class ReportDTO : BaseEntityDTO
    {
        public Guid PostId { get; set; } = default!;  
        public String ReportingUserDisplayName { get; set; } = default!;
        public String Text { get; set; } = default!;
        public bool Closed { get; set; } = default!;
    }
}

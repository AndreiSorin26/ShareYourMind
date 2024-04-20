using Services.DTOs.EntityMappings;

namespace Services.DTOs
{
    public class NewReportDTO
    {
        public PostDTO ReportedPost { get; set; } = default!;
        public String Text { get; set; } = default!;
    }
}

using Services.DTOs;
using Services.DTOs.EntityMappings;

namespace Services.Services.Interfaces
{
    public interface IReportService
    {
        public void ReportPost(NewReportDTO report, UserDTO reportingUser);
        public void UpdateReport(ReportDTO report);
        public IEnumerable<ReportDTO> GetReportedPosts();
    }
}

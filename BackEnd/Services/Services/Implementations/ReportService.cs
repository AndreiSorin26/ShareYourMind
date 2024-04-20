using Database.Entities;
using Database.Repositories.Interfaces;
using Services.DTOs;
using Services.DTOs.EntityMappings;
using Services.Exceptions;
using Services.Services.Interfaces;

using AutoMapper;

namespace Services.Services.Implementations
{
    public class ReportService(IReportRepository m_reportRepository, IMapper m_mapper) : IReportService
    {
        public IEnumerable<ReportDTO> GetReportedPosts()
        {
            IEnumerable<Report> reportedPosts = m_reportRepository.GetReportes();
            return m_mapper.Map<IEnumerable<ReportDTO>>(reportedPosts);
        }

        public void ReportPost(NewReportDTO report, UserDTO reportingUser)
        {
            try
            {
                Report newReport = new()
                {
                    ReportingUserId = reportingUser.Id,
                    PostId = report.ReportedPost.Id,
                    Text = report.Text,
                    Closed = false
                };
                m_reportRepository.Add(newReport);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Report cannot be null");
            }
        }

        public void UpdateReport(ReportDTO report)
        {
            try
            {
                Report oldReport = m_reportRepository.Get(report.Id);
                oldReport.Closed = true;
                oldReport.UpdateTime();
                m_reportRepository.Update(oldReport);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Report cannot be null");
            }
            catch (NullReferenceException)
            {
                throw new EntityNotFound("Report not found");
            }
        }
    }
}

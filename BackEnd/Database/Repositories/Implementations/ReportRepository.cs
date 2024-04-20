using Database.Entities;
using Database.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.Implementations
{
    public class ReportRepository(DatabaseContext m_dbContext) : IReportRepository
    {
        public void Add(Report report)
        {
            ArgumentNullException.ThrowIfNull(report);

            m_dbContext.Reports.Add(report);
            m_dbContext.SaveChanges();
        }

        public Report Get(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id);
            Report report = m_dbContext.Reports.Find(id) ?? throw new NullReferenceException("Report not found");
            return report;
        }

        public IEnumerable<Report> GetReportes()
        {
            return m_dbContext.Reports.Include(report => report.Reporter).Include(report => report.Post).Where(report => !report.Closed);
        }

        public void Update(Report report)
        {
            ArgumentNullException.ThrowIfNull(report);

            Report existentReport = m_dbContext.Reports.Find(report.Id) ?? throw new NullReferenceException("Report not found");
            m_dbContext.Reports.Entry(existentReport).CurrentValues.SetValues(report);
            m_dbContext.SaveChanges();
        }
    }
}

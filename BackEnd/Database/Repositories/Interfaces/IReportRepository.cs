using Database.Entities;

namespace Database.Repositories.Interfaces
{
    public interface IReportRepository
    {
        public Report Get(Guid id);
        public void Add(Report report);
        public void Update(Report report);
        public IEnumerable<Report> GetReportes();
    }
}

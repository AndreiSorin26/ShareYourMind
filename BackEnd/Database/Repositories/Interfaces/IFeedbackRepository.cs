using Database.Entities;

namespace Database.Repositories.Interfaces
{
    public interface IFeedbackRepository
    {
        public Feedback GetById(Guid id);
        public IEnumerable<Feedback> GetByUser(Guid userId);
        public IEnumerable<Feedback> GetAll();
        public Feedback? GetLastFeedbackforUser(Guid userId);
        public void Add(Feedback feedback);
    }
}

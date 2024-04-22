using Database.Entities;
using Database.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.Implementations
{
    public class FeedbackRepository(DatabaseContext m_dbContext) : IFeedbackRepository
    {
        public void Add(Feedback feedback)
        {
            ArgumentNullException.ThrowIfNull(feedback);

            m_dbContext.Feedbacks.Add(feedback);
            m_dbContext.SaveChanges();
        }

        public IEnumerable<Feedback> GetAll()
        {
            return [.. m_dbContext.Feedbacks.Include(feedback => feedback.User)];
        }

        public Feedback GetById(Guid id)
        {
            Feedback feedback = m_dbContext.Feedbacks.Include(feedback => feedback.User).FirstOrDefault(feedback => feedback.Id == id) ?? throw new NullReferenceException("Feedback not found");
            return feedback;
        }

        public IEnumerable<Feedback> GetByUser(Guid userId)
        {
            ArgumentNullException.ThrowIfNull(userId);

            return m_dbContext.Feedbacks.Include(feedback => feedback.User).Where(feedback => feedback.UserId == userId);
        }

        public Feedback? GetLastFeedbackforUser(Guid userId)
        {
            ArgumentNullException.ThrowIfNull(userId);

            return m_dbContext.Feedbacks.FirstOrDefault(feeback => feeback.UserId == userId);
        }
    }
}

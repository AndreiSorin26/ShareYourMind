using Services.DTOs;
using Services.DTOs.EntityMappings;

namespace Services.Services.Interfaces
{
    public interface IFeedbackService
    {
        public IEnumerable<FeedbackDTO> GetFedback(DateTime startDate, DateTime endDate, int maxCount = 20);
        public IEnumerable<FeedbackDTO> GetBadFeedback(DateTime startDate, DateTime endDate);
        public IEnumerable<FeedbackDTO> GetGoodFeedback(DateTime startDate, DateTime endDate);
        public IEnumerable<FeedbackMetric> GetFeedbackMetrics(DateTime startDate, DateTime endDate);
        public void AddFeedback(NewFeedbackDTO feedbackDTO, string userDisplayName);
    }
}

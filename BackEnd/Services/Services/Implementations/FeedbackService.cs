using Database.Entities;
using Database.Repositories.Interfaces;
using Services.DTOs;
using Services.DTOs.EntityMappings;
using Services.Services.Interfaces;

using AutoMapper;

namespace Services.Services.Implementations
{
    public class FeedbackService(IFeedbackRepository m_feedbackRepository, IUserRepository m_userRepository, IMapper m_mapper) : IFeedbackService
    {
        public IEnumerable<FeedbackDTO> GetBadFeedback(DateTime startDate, DateTime endDate)
        {
            IEnumerable<Feedback> feedbacks = m_feedbackRepository.GetAll()
                                                                  .Where(feedback => feedback.UpdatedAt.CompareTo(startDate) >= 0 && feedback.UpdatedAt.CompareTo(endDate) <= 0)
                                                                  .Where(feedback => (feedback.UIRating + feedback.UXRating + feedback.CommunityRating + feedback.DataFlowRating) / 4 <= 2.5);
            return m_mapper.Map<IEnumerable<FeedbackDTO>>(feedbacks);
        }

        public IEnumerable<FeedbackMetric> GetFeedbackMetrics(DateTime startDate, DateTime endDate)
        {
            IEnumerable<Feedback> feedbacks = m_feedbackRepository.GetAll()
                                                                  .Where(feedback => feedback.UpdatedAt.CompareTo(startDate) >= 0 && feedback.UpdatedAt.CompareTo(endDate) <= 0);

            List<FeedbackMetric> metrics =
                [
                    new FeedbackMetric() { Name = "UX Rating", Type = "Average", Value = feedbacks.Average(feedback => feedback.UXRating) },
                    new FeedbackMetric() { Name = "UI Rating", Type = "Average", Value = feedbacks.Average(feedback => feedback.UIRating) },
                    new FeedbackMetric() { Name = "Comunity Rating", Type = "Average", Value = feedbacks.Average(feedback => feedback.CommunityRating) },
                    new FeedbackMetric() { Name = "Dataflow Rating", Type = "Average", Value = feedbacks.Average(feedback => feedback.DataFlowRating) },
                    
                    new FeedbackMetric() { Name = "5 UI Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.UIRating >= 5.0) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "4 UI Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.UIRating >= 4.0 && feedback.UIRating < 5) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "3 UI Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.UIRating >= 3.0 && feedback.UIRating < 4) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "2 UI Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.UIRating >= 2.0 && feedback.UIRating < 3) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "1 UI Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.UIRating >= 1.0 && feedback.UIRating < 2) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "0 UI Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.UIRating >= 0 && feedback.UIRating < 1) / feedbacks.Count() },

                    new FeedbackMetric() { Name = "5 UX Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.UXRating >= 5.0) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "4 UX Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.UXRating >= 4.0 && feedback.UXRating < 5) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "3 UX Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.UXRating >= 3.0 && feedback.UXRating < 4) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "2 UX Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.UXRating >= 2.0 && feedback.UXRating < 3) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "1 UX Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.UXRating >= 1.0 && feedback.UXRating < 2) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "0 UX Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.UXRating >= 0 && feedback.UXRating < 1) / feedbacks.Count() },

                    new FeedbackMetric() { Name = "5 Community Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.CommunityRating >= 5.0) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "4 Community Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.CommunityRating >= 4.0 && feedback.CommunityRating < 5) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "3 Community Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.CommunityRating >= 3.0 && feedback.CommunityRating < 4) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "2 Community Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.CommunityRating >= 2.0 && feedback.CommunityRating < 3) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "1 Community Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.CommunityRating >= 1.0 && feedback.CommunityRating < 2) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "0 Community Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.CommunityRating >= 0 && feedback.CommunityRating < 1) / feedbacks.Count() },

                    new FeedbackMetric() { Name = "5 DataFlow Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.DataFlowRating >= 5.0) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "4 DataFlow Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.DataFlowRating >= 4.0 && feedback.DataFlowRating < 5) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "3 DataFlow Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.DataFlowRating >= 3.0 && feedback.DataFlowRating < 4) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "2 DataFlow Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.DataFlowRating >= 2.0 && feedback.DataFlowRating < 3) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "1 DataFlow Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.DataFlowRating >= 1.0 && feedback.DataFlowRating < 2) / feedbacks.Count() },
                    new FeedbackMetric() { Name = "0 DataFlow Rating Users", Type = "Percentage", Value = (float)feedbacks.Count(feedback => feedback.DataFlowRating >= 0 && feedback.DataFlowRating < 1) / feedbacks.Count() }
                ];

            return metrics;
        }

        public IEnumerable<FeedbackDTO> GetGoodFeedback(DateTime startDate, DateTime endDate)
        {
            IEnumerable<Feedback> feedbacks = m_feedbackRepository.GetAll()
                                                                  .Where(feedback => feedback.UpdatedAt.CompareTo(startDate) >= 0 && feedback.UpdatedAt.CompareTo(endDate) <= 0)
                                                                  .Where(feedback => (feedback.UIRating + feedback.UXRating + feedback.CommunityRating + feedback.DataFlowRating) / 4 > 2.5);
            return m_mapper.Map<IEnumerable<FeedbackDTO>>(feedbacks);
        }

        public IEnumerable<FeedbackDTO> GetFedback(DateTime startDate, DateTime endDate, int maxCount)
        {
            IEnumerable<Feedback> feedbacks = m_feedbackRepository.GetAll()
                                                                  .Where(feedback => feedback.UpdatedAt.CompareTo(startDate) >= 0 && feedback.UpdatedAt.CompareTo(endDate) <= 0)
                                                                  .OrderByDescending(feedback => feedback.UpdatedAt)
                                                                  .Take(maxCount);
            

            return m_mapper.Map<IEnumerable<FeedbackDTO>>(feedbacks);
        }

        public void AddFeedback(NewFeedbackDTO feedbackDTO, String userDisplayName)
        {
            User user = m_userRepository.GetUserByDisplayName(userDisplayName);

            Feedback? lastFeedback = m_feedbackRepository.GetLastFeedbackforUser(user.Id);
            if (lastFeedback != null && (DateTime.Now - lastFeedback.CreatedAt).TotalDays <= 1) 
                throw new Exception("Cannot post more than one feedback a day! Don't spam dude ://");
            

            Feedback feedback = new()
            {
                UserId = user.Id,
                Text = feedbackDTO.Text,
                UIRating = feedbackDTO.UIRating,
                UXRating = feedbackDTO.UXRating,
                CommunityRating = feedbackDTO.CommunityRating,
                DataFlowRating = feedbackDTO.DataFlowRating
            };

            m_feedbackRepository.Add(feedback);
        }
    }
}

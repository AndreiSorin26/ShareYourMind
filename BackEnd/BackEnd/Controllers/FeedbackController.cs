using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.DTOs.EntityMappings;
using Services.Exceptions;
using Services.Services.Interfaces;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController(IFeedbackService m_feedbackService) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("good/{start:datetime}/{end:datetime}")]
        public ActionResult<IEnumerable<FeedbackDTO>> GetGoodFeedbacks(DateTime start, DateTime end)
        {
            try
            {
                IEnumerable<FeedbackDTO> goodFeedbacks = m_feedbackService.GetGoodFeedback(start, end);
                return Ok(goodFeedbacks);
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    BadQuery bq => BadRequest(bq.Message),
                    EntityNotFound => NotFound(),
                    _ => StatusCode(500, ex.Message),
                };
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("bad/{start:datetime}/{end:datetime}")]
        public ActionResult<IEnumerable<FeedbackDTO>> GetBadFeedbacks(DateTime start, DateTime end)
        {
            try
            {
                IEnumerable<FeedbackDTO> goodFeedbacks = m_feedbackService.GetBadFeedback(start, end);
                return Ok(goodFeedbacks);
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    BadQuery bq => BadRequest(bq.Message),
                    EntityNotFound => NotFound(),
                    _ => StatusCode(500, ex.Message),
                };
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{start:datetime}/{end:datetime}/{maxCount:int}")]
        public ActionResult<IEnumerable<FeedbackDTO>> GetBadFeedbacks(DateTime start, DateTime end, int maxCount)
        {
            try
            {
                IEnumerable<FeedbackDTO> goodFeedbacks = m_feedbackService.GetFedback(start, end, maxCount);
                return Ok(goodFeedbacks);
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    BadQuery bq => BadRequest(bq.Message),
                    EntityNotFound => NotFound(),
                    _ => StatusCode(500, ex.Message),
                };
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("metrics/{start:datetime}/{end:datetime}")]
        public ActionResult<IEnumerable<FeedbackMetric>> GetFeedbackMetrics(DateTime start, DateTime end)
        {
            try
            {
                IEnumerable<FeedbackMetric> feedbackMetrics = m_feedbackService.GetFeedbackMetrics(start, end);
                return Ok(feedbackMetrics);
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    BadQuery bq => BadRequest(bq.Message),
                    EntityNotFound => NotFound(),
                    _ => StatusCode(500, ex.Message),
                };
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult AddFeedback([FromBody] NewFeedbackDTO newFeedback)
        {
            var displayName = User.FindFirstValue(ClaimTypes.Name);
            if (displayName == null)
                return Unauthorized("Invalid token");

            try
            {
                m_feedbackService.AddFeedback(newFeedback, displayName);
                return Ok();
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    BadQuery bq => BadRequest(bq.Message),
                    EntityNotFound => NotFound(),
                    _ => StatusCode(500, ex.Message),
                };
            }
        }
    }
}

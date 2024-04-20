using Services.DTOs;
using Services.Exceptions;
using Services.Services.Interfaces;
using Services.Interfaces;
using Services.DTOs.EntityMappings;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController(IReportService m_reportService, IUserService m_userService) : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public ActionResult ReportPost([FromBody] NewReportDTO newReport)
        {
            var displayName = User.FindFirstValue(ClaimTypes.Name);
            if (displayName == null)
                return Unauthorized("Invalid token");

            try
            {
                UserDTO user = m_userService.Get(displayName);
                m_reportService.ReportPost(newReport, user);
                return Ok(true);
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
        [HttpPut]
        public ActionResult UpdatePost([FromBody] ReportDTO report)
        {
            var displayName = User.FindFirstValue(ClaimTypes.Name);
            if (displayName == null)
                return Unauthorized("Invalid token");

            try
            {
                m_reportService.UpdateReport(report);
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult GetReportedPost()
        {
            try
            {
                IEnumerable<ReportDTO> reportedPosst = m_reportService.GetReportedPosts();
                return Ok(reportedPosst);
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

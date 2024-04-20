using Services.DTOs.EntityMappings;
using Services.Services.Interfaces;
using Services.Exceptions;
using Services.DTOs;
using System.Security.Claims;
using Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Implementations;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(ICommentService m_commentService, IUserService m_userService) : ControllerBase
    {
        [Authorize]
        [HttpGet("{postId:guid}")]
        public ActionResult<CommentDTO> GetPostComments(Guid postId)
        {
            try
            {
                IEnumerable<CommentDTO> postComments = m_commentService.Get(postId);
                return Ok(postComments);
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

        [Authorize]
        [HttpPost]
        public ActionResult<CommentDTO> AddComment([FromBody] NewCommentDTO comment)
        {
            var displayName = User.FindFirstValue(ClaimTypes.Name);
            if (displayName == null)
                return Unauthorized("Invalid token");

            try
            {
                CommentDTO newComment = m_commentService.Add(comment, displayName);
                return Ok(newComment);
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

        [Authorize]
        [HttpDelete("{id:guid}")]
        public ActionResult DeleteComment([FromBody] Guid id)
        {
            var displayName = User.FindFirstValue(ClaimTypes.Name);
            if (displayName == null)
                return Unauthorized("Invalid token");

            try
            {
                UserDTO user = m_userService.Get(displayName);
                CommentDTO comment = m_commentService.GetOne(id);

                if(comment.PosterDisplayName != displayName)
                    return Forbid("Cannot delete other people's comments. Don't be rude man ://");

                m_commentService.Delete(id);
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

        [Authorize]
        [HttpPut]
        public ActionResult UpdateComment([FromBody] CommentDTO newComment)
        {
            var displayName = User.FindFirstValue(ClaimTypes.Name);
            if (displayName == null)
                return Unauthorized("Invalid token");

            try
            {
                CommentDTO comment = m_commentService.GetOne(newComment.Id);

                if (comment.PosterDisplayName != displayName)
                    return Forbid("Cannot edit other people's posts. Don't be rude man ://");

                m_commentService.Update(newComment);
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

using Database.Entities;
using Services.Exceptions;
using Services.Interfaces;
using Services.DTOs;
using Services.DTOs.EntityMappings;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController(IPostService m_postService, IUserService m_userService) : ControllerBase
    {
        [Authorize]
        [HttpGet("feed/{batchIndex:int}")]
        public ActionResult<IEnumerable<PostDTO>> GetFeedPosts(int batchIndex)
        {
            var displayName = User.FindFirstValue(ClaimTypes.Name);
            if (displayName == null)
                return Unauthorized("Invalid token");

            try
            {
                UserDTO user = m_userService.Get(displayName);
                IEnumerable<PostDTO> feedPosts = m_postService.GetFeedPosts(user.Id, batchIndex);
                return Ok(feedPosts);
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
        [HttpGet("personal/{batchIndex:int}")]
        public ActionResult<IEnumerable<PostDTO>> GetPersonalPosts(int batchIndex)
        {
            var displayName = User.FindFirstValue(ClaimTypes.Name);
            if (displayName == null)
                return Unauthorized("Invalid token");

            try
            {
                UserDTO user = m_userService.Get(displayName);
                IEnumerable<PostDTO> personalPosts = m_postService.GetPersonalPosts(user.Id, batchIndex);
                return Ok(personalPosts);
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
        public ActionResult<Post> CreatePost([FromBody] NewPostDTO newPost)
        {
            var displayName = User.FindFirstValue(ClaimTypes.Name);
            if (displayName == null)
                return Unauthorized("Invalid token");

            try
            {
                m_postService.Add(newPost, displayName);
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
        [HttpDelete("{id:guid}")]
        public ActionResult DeletePost(Guid id)
        {
            var displayName = User.FindFirstValue(ClaimTypes.Name);
            if (displayName == null)
                return Unauthorized("Invalid token");

            try
            {
                PostDTO post = m_postService.Get(id);
                
                if(post.PosterDisplayName != displayName)
                    return Forbid("Cannot delete other people's posts. Don't be rude man ://");

                m_postService.Delete(id);
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
        [HttpDelete("force/{id:guid}")]
        public ActionResult ForceDeletePost(Guid id)
        {
            try
            {
                m_postService.Delete(id);
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
        public ActionResult UpdatePost([FromBody] PostDTO newPost)
        {
            var displayName = User.FindFirstValue(ClaimTypes.Name);
            if (displayName == null)
                return Unauthorized("Invalid token");

            try
            {
                PostDTO post = m_postService.Get(newPost.Id);

                if (post.PosterDisplayName != displayName)
                    return Forbid("Cannot edit other people's posts. Don't be rude man ://");

                m_postService.Update(newPost);
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
        [HttpGet("{postId:guid}")]
        public ActionResult<PostDTO> GetPost(Guid postId)
        {
            try
            {
                PostDTO post = m_postService.Get(postId);
                return Ok(post);
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

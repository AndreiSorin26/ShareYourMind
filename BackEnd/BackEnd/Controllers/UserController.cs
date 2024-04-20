using Services.DTOs.EntityMappings;
using Services.Exceptions;
using Services.Interfaces;
using Services.DTOs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Database.Entities.Enums;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService m_userService, IPostService m_postService) : Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult<UserDTO> GetUser()
        {   
            var displayName = User.FindFirstValue(ClaimTypes.Name);
            if (displayName == null)
                return Unauthorized("Invalid token");

            try
            {
                UserDTO user = m_userService.Get(displayName);
                return Ok(user);
            }
            catch(Exception ex)
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
        [HttpGet("admin/unapproved")]
        public ActionResult<IEnumerable<UserDTO>> GetAdminRequests()
        {
            try
            {
                IEnumerable<UserDTO> unapporvedAdmins = m_userService.GetUnapprovedAdmins();
                return Ok(unapporvedAdmins);
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

        [HttpPost]
        public ActionResult<UserDTO> CreateUser([FromBody] NewUserDTO signupRequest)
        {
            try
            {
                UserDTO newUser = m_userService.Add(signupRequest);
                return Ok(newUser);
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
        [HttpPut("admin/approve")]
        public ActionResult ApproveAdminRequest([FromBody] UserDTO adminDTO)
        {
            try
            {
                UserDTO admin = m_userService.Get(adminDTO.DisplayName);
                m_userService.ApproveAdmin(admin);
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
        [HttpPut("admin/close-report/{reportId:guid}")]
        public ActionResult ApproveAdminRequest(Guid reportId)
        {
            try
            {
                m_userService.CloseReport(reportId);
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
        public ActionResult DeleteUser(Guid id)
        {
            try
            {
                UserDTO admin = m_userService.Get(id);
                m_userService.ApproveAdmin(admin);
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
        [HttpPost("friend")]
        public ActionResult SendFriendRequest([FromBody] NewFriendRequestDTO fr)
        {
            var displayName = User.FindFirstValue(ClaimTypes.Name);
            if (displayName == null)
                return Unauthorized("Invalid token");

            try
            {
                UserDTO sender = m_userService.Get(displayName);
                UserDTO receiver = m_userService.Get(fr.ReceiverDisplayName);
                FriendRequestDTO newFr = m_userService.SendFriendRequest(sender, receiver);
                return Ok(newFr);        
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
        [HttpPut("friend")]
        public ActionResult UpdateFriendRequest([FromBody] FriendRequestDTO frDTO)
        {
            try
            {
                m_userService.UpdateFriendRequest(frDTO.Id, frDTO.Status);
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
        [HttpGet("friend")]
        public ActionResult GetFriendRequest()
        {
            var displayName = User.FindFirstValue(ClaimTypes.Name);
            if (displayName == null)
                return Unauthorized("Invalid token");

            try
            {
                UserDTO user = m_userService.Get(displayName);
                IEnumerable<FriendRequestDTO> frs = m_userService.GetFriendRequests(user.Id);
                return Ok(frs);
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
        [HttpPost("react")]
        public ActionResult ReactToPost([FromBody] NewReactionDTO newReaction)
        {
            var displayName = User.FindFirstValue(ClaimTypes.Name);
            if (displayName == null)
                return Unauthorized("Invalid token");

            if (newReaction.ReactionType != "Love" && newReaction.ReactionType != "Laugh" && newReaction.ReactionType != "Dislike")
                return BadRequest("The allowed reaction types are ['Love', 'Laugh', 'Dislike']");

            try
            {
                PostDTO post = m_postService.Get(newReaction.PostId);

                m_userService.HandleUserReaction(displayName, post, (ReactionType)Enum.Parse(typeof(ReactionType), newReaction.ReactionType));
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

using Database.Entities;
using Database.Repositories.Interfaces;
using Services.Exceptions;
using Services.Services.Interfaces;
using Services.DTOs.EntityMappings;
using Services.DTOs;

using AutoMapper;

namespace Services.Services.Implementations
{
    public class CommentService(ICommentRepository m_commentRepository, IUserRepository m_userRepository, IMapper m_mapper) : ICommentService
    {
        public CommentDTO Add(NewCommentDTO comment, String senderDisplayName)
        {
            try
            {
                User user = m_userRepository.GetUserByDisplayName(senderDisplayName);
                Comment newComment = new()
                {
                    UserId = user.Id,
                    PostId = comment.PostId,
                    Text = comment.Text
                };

                m_commentRepository.Add(newComment);
                return m_mapper.Map<CommentDTO>(newComment);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Comment cannot be null");
            }
        }

        public void Delete(Guid commentId)
        {
            try
            {
                m_commentRepository.Delete(commentId);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Id cannot be null");
            }
            catch (NullReferenceException)
            {
                throw new EntityNotFound("Comment not found");
            }
        }

        public IEnumerable<CommentDTO> Get(Guid postId)
        {
            try
            {
                IEnumerable<Comment> postComments = m_commentRepository.Get(postId);
                return m_mapper.Map<IEnumerable<CommentDTO>>(postComments);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Id cannot be null");
            }
        }

        public CommentDTO GetOne(Guid commentId)
        {
            try
            {
                Comment comment = m_commentRepository.GetOne(commentId);
                return m_mapper.Map<CommentDTO>(comment);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Id cannot be null");
            }
            catch (NullReferenceException)
            {
                throw new EntityNotFound("Comment not found");
            }
        }

        public void Update(CommentDTO newComment)
        {
            try
            {
                Comment oldComment = m_commentRepository.GetOne(newComment.Id);
                oldComment.Text = newComment.Text;
                oldComment.UpdateTime();
                m_commentRepository.Update(oldComment);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Commnet cannot be null");
            }
        }
    }
}

using Database.Entities;
using Database.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.Implementations
{
    public class CommentRepository(DatabaseContext m_dbContext) : ICommentRepository
    {
        public void Add(Comment comment)
        {
            ArgumentNullException.ThrowIfNull(comment);

            m_dbContext.Comments.Add(comment);
            m_dbContext.SaveChanges();
        }

        public void Delete(Guid commentId)
        {
            Comment comment = m_dbContext.Comments.Find(commentId) ?? throw new NullReferenceException("Comment not found");
            m_dbContext.Comments.Remove(comment);
            m_dbContext.SaveChanges();
        }

        public IEnumerable<Comment> Get(Guid postId)
        {
            return m_dbContext.Comments.Where(comment =>  comment.PostId == postId).Include(comment => comment.Poster).OrderByDescending(comment => comment.UpdatedAt);
        }

        public void Update(Comment newComment)
        {
            Comment comment = m_dbContext.Comments.Find(newComment.Id) ?? throw new NullReferenceException("Comment not found");
            m_dbContext.Comments.Entry(comment).CurrentValues.SetValues(newComment);
            m_dbContext.SaveChanges();
        }

        public Comment GetOne(Guid commentId)
        {
            ArgumentNullException.ThrowIfNull(commentId);

            return m_dbContext.Comments.Find(commentId) ?? throw new NullReferenceException("Comment not found");
        }
    }
}

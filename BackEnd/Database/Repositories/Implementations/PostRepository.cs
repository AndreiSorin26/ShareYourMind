using Database.Entities;
using Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.Implementations
{
    public class PostRepository(DatabaseContext m_dbContext) : IPostRepository
    {
        public void Add(Post post)
        {
            ArgumentNullException.ThrowIfNull(post);

            m_dbContext.Posts.Add(post);
            m_dbContext.SaveChanges();
        }

        public void Delete(Post post)
        {
            ArgumentNullException.ThrowIfNull(post);

            Post existentPost = m_dbContext.Posts.Find(post.Id) ?? throw new NullReferenceException();
            m_dbContext.Posts.Remove(existentPost);
            m_dbContext.SaveChanges();
        }

        public Post GetById(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id);

            return m_dbContext.Posts.Include(post => post.Poster)
                                    .Include(post => post.LoveReactionUsers)
                                    .Include(post => post.LaughReactionUsers)
                                    .Include(post => post.DislikeReactionUsers)
                                    .FirstOrDefault(post => post.Id == id) ?? throw new NullReferenceException();
        }

        public IEnumerable<Post> GetUserPosts(Guid userId)
        {
            ArgumentNullException.ThrowIfNull(userId);

            return m_dbContext.Posts.Include(post => post.Poster)
                                    .Include(post => post.LoveReactionUsers)
                                    .Include(post => post.LaughReactionUsers)
                                    .Include(post => post.DislikeReactionUsers)
                                    .Where(post => post.UserId == userId);
        }

        public IEnumerable<Post> GetUsersPosts(IEnumerable<Guid> userIds)
        {
            ArgumentNullException.ThrowIfNull(userIds);

            return m_dbContext.Posts.Include(post => post.Poster)
                                    .Include(post => post.LoveReactionUsers)
                                    .Include(post => post.LaughReactionUsers)
                                    .Include(post => post.DislikeReactionUsers)
                                    .Where(post => userIds.Contains(post.UserId));
        }

        public void Update(Post newPost)
        {
            ArgumentNullException.ThrowIfNull(newPost);

            Post oldPost = m_dbContext.Posts.Find(newPost.Id) ?? throw new NullReferenceException();
            m_dbContext.Posts.Entry(oldPost).CurrentValues.SetValues(newPost);
            m_dbContext.SaveChanges();
        }
    }
}

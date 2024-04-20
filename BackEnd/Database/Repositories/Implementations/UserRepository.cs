using Database.Entities;
using Database.Entities.Enums;
using Database.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.Implementations
{   
    public class UserRepository(DatabaseContext m_dbContext) : IUserRepository
    {
        public void Add(User user) 
        {
            m_dbContext.Users.Add(user);
            m_dbContext.SaveChanges();
        }

        public User GetUserByDisplayName(string displayName)
        {
            ArgumentNullException.ThrowIfNull(m_dbContext);

            var user = m_dbContext.Users
                                  .Include(user => user.LoveReactionPosts)
                                  .Include(user => user.LaughReactionPosts)
                                  .Include(user => user.DislikeReactionPosts)
                                  .FirstOrDefault(u => u.DisplayName == displayName);

            return user ?? throw new NullReferenceException();
        }

        public User GetUserById(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id);

            var user = m_dbContext.Users
                                  .Include(user => user.LoveReactionPosts)
                                  .Include(user => user.LaughReactionPosts)
                                  .Include(user => user.DislikeReactionPosts)
                                  .FirstOrDefault(user => user.Id == id);

            return user ?? throw new NullReferenceException();
        }

        public void Update(User newUser) 
        {
            ArgumentNullException.ThrowIfNull(newUser);

            var user = m_dbContext.Users.Find(newUser.Id) ?? throw new NullReferenceException();
            m_dbContext.Users.Entry(user).CurrentValues.SetValues(newUser);
            m_dbContext.SaveChanges();
        }

        public void Delete(String displayname) 
        {
            ArgumentNullException.ThrowIfNull(displayname);

            var user = m_dbContext.Users.FirstOrDefault(user => user.DisplayName == displayname) ?? throw new NullReferenceException();
            m_dbContext.Remove(user);
            m_dbContext.SaveChanges();
        }

        public IEnumerable<User> GetUnapprovedAdmins()
        {
            var newAdmins = m_dbContext.Users.Where(user => user.Role == UserRole.Admin && !user.Approved);
            return newAdmins;
        }

        public void HandleUserReaction(User user, Post post, ReactionType reactionType)
        {
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNull(post);

            switch (reactionType) 
            {
                case ReactionType.Love:

                    user.LaughReactionPosts.Remove(post);
                    user.DislikeReactionPosts.Remove(post);
                    if (!user.LoveReactionPosts.Remove(post))
                        user.LoveReactionPosts.Add(post);
                    break;
                case ReactionType.Laugh:
                    user.LoveReactionPosts.Remove(post);
                    user.DislikeReactionPosts.Remove(post);
                    if (!user.LaughReactionPosts.Remove(post))
                        user.LaughReactionPosts.Add(post);
                    break;
                case ReactionType.Dislike:
                    user.LaughReactionPosts.Remove(post);
                    user.LoveReactionPosts.Remove(post);
                    if (!user.DislikeReactionPosts.Remove(post))
                        user.DislikeReactionPosts.Add(post);
                    break;
            }
            m_dbContext.SaveChanges();
        }
    }
}
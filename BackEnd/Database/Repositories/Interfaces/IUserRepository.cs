using Database.Entities.Enums;
using Database.Entities;

namespace Database.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public void Add(User user);
        public User GetUserByDisplayName(string displayName);
        public User GetUserById(Guid id);
        public void Update(User newUser);
        public void Delete(String displayName);
        public IEnumerable<User> GetUnapprovedAdmins();
        public void HandleUserReaction(User user, Post post, ReactionType reactionType);
    }
}

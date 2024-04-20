using Database.Entities.Enums;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

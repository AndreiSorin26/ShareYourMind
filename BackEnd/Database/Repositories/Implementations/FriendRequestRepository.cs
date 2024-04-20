using Database.Entities;
using Database.Entities.Enums;
using Database.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.Implementations
{
    public class FriendRequestRepository(DatabaseContext m_dbContext) : IFriendRequestRepository
    {
        public FriendRequest Get(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id);

            return m_dbContext.FriendRequests.Find(id) ?? throw new NullReferenceException("Friend request not found");
        }

        public void Add(FriendRequest fr)
        {
            ArgumentNullException.ThrowIfNull(fr);

            m_dbContext.FriendRequests.Add(fr);
            m_dbContext.SaveChanges();
        }

        public void Delete(FriendRequest fr)
        {
            ArgumentNullException.ThrowIfNull(fr);

            FriendRequest existentFriendRequest = m_dbContext.FriendRequests.Find(fr.Id) ?? throw new NullReferenceException("Friend request not found");
            m_dbContext.FriendRequests.Remove(existentFriendRequest);
            m_dbContext.SaveChanges();
        }

        public void Update(FriendRequest newFr)
        {
            ArgumentNullException.ThrowIfNull(newFr);
            
            FriendRequest fr = m_dbContext.FriendRequests.Find(newFr.Id) ?? throw new NullReferenceException("Friend request not found");
            m_dbContext.FriendRequests.Entry(fr).CurrentValues.SetValues(newFr);
            m_dbContext.SaveChanges();
        }

        public IEnumerable<FriendRequest> GetReceivedFriendsRequests(Guid userId)
        {
            ArgumentNullException.ThrowIfNull(userId);

            return m_dbContext.FriendRequests.Where(fr => fr.ReceiverId == userId).Include(fr => fr.Sender);
        }

        public IEnumerable<FriendRequest> GetSentFriendsRequests(Guid userId)
        {
            ArgumentNullException.ThrowIfNull(userId);

            return m_dbContext.FriendRequests.Where(fr => fr.SenderId == userId).Include(fr => fr.Receiver);
        }

        public IEnumerable<FriendRequest> GetAcceptedFriendRequests(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id);

            return m_dbContext.FriendRequests.Where(fr => fr.SenderId == id || fr.ReceiverId == id && fr.Status == FriendRequestStatus.ACCEPTED)
                                             .Include(fr => fr.Sender)
                                             .Include(fr => fr.Receiver);
        }
    }
}

using Database.Entities;

namespace Database.Repositories.Interfaces
{
    public interface IFriendRequestRepository
    {
        public FriendRequest Get(Guid id);
        public void Add(FriendRequest fr);
        public void Delete(FriendRequest fr);
        public void Update(FriendRequest newFr);
        public IEnumerable<FriendRequest> GetReceivedFriendsRequests(Guid id);
        public IEnumerable<FriendRequest> GetSentFriendsRequests(Guid id);
        public IEnumerable<FriendRequest> GetAcceptedFriendRequests(Guid id);
    }
}

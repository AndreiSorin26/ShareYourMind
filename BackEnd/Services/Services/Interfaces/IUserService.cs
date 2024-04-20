using Database.Entities;
using Database.Entities.Enums;
using Services.DTOs;
using Services.DTOs.EntityMappings;

namespace Services.Interfaces
{
    public interface IUserService
    {
        public UserDTO Add(NewUserDTO user);
        public UserDTO Get(String displayName);
        public User GetRaw(String displayName);
        public UserDTO Get(Guid id);
        public IEnumerable<UserDTO> GetUnapprovedAdmins();
        public void ApproveAdmin(UserDTO user);
        public FriendRequestDTO SendFriendRequest(UserDTO sender, UserDTO receiver);
        public void UpdateFriendRequest(Guid id, FriendRequestStatus status);
        public IEnumerable<FriendRequestDTO> GetFriendRequests(Guid id);
        public void HandleUserReaction(string displayName, PostDTO postDTO, ReactionType reactionType);
        public void CloseReport(Guid reportId);
    }
}

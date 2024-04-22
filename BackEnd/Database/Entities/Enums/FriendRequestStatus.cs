using System.ComponentModel;

namespace Database.Entities.Enums
{
    public enum FriendRequestStatus
    {
        [Description("Accepted")]
        ACCEPTED,
        [Description("Pending")]
        PENDING,
        [Description("Refused")]
        REFUSED
    }
}

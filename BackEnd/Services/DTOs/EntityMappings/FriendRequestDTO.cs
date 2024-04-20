using Database.Entities.Enums;

namespace Services.DTOs.EntityMappings
{
    public class FriendRequestDTO : BaseEntityDTO
    {
        public string SenderDisplayName { get; set; } = default!;
        public string ReceiverDisplayName { get; set; } = default!;
        public FriendRequestStatus Status { get; set; } = default!;
    }
}

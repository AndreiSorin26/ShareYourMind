using Database.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class FriendRequest : BaseEntity
    {
        public Guid SenderId { get; set; } = default!;
        public Guid ReceiverId { get; set; } = default!;
        public FriendRequestStatus Status { get; set; } = default!;

        //Entities tha are referenced by the foreign keys
        public User Sender { get; set; } = default!;
        public User Receiver { get; set; } = default!;
    }
}

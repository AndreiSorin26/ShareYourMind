using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

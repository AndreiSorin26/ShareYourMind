using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entities.Enums;

namespace Database.Entities
{
    public class User : BaseEntity
    {
        public String Username { get; set; } = default!;
        public String Password { get; set; } = default!;
        public byte[] Salt { get; set; } = default!;
        public String Tag { get; set; } = default!;
        public String DisplayName { get; set; } = default!;
        public UserRole Role { get; set; } = default!;
        public bool Approved { get; set; } = default!;
        public ICollection<Post> LoveReactionPosts { get; set; } = default!;
        public ICollection<Post> LaughReactionPosts { get; set; } = default!;
        public ICollection<Post> DislikeReactionPosts { get; set; } = default!;

        //Entities tha are referenced by the foreign keys
        public IEnumerable<FriendRequest> ReceivedFriendRequests { get; set; } = default!;
        public IEnumerable<FriendRequest> SentFriendRequests { get; set; } = default!;
        public IEnumerable<Post> Posts { get; set; } = default!;
        public IEnumerable<Comment> Comments { get; set; } = default!;
        public IEnumerable<Report> Reports { get; set; } = default!;
        public IEnumerable<Feedback> Feedbacks { get; set; } = default!;
    }
}

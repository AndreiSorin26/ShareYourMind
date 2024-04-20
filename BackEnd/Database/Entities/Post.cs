namespace Database.Entities
{
    public class Post : BaseEntity
    {
        public Guid UserId { get; set; } = default!;
        public String Text { get; set; } = default!;

        //Entities tha are referenced by the foreign keys
        public ICollection<User> LoveReactionUsers { get; set; } = default!;
        public ICollection<User> LaughReactionUsers { get; set; } = default!;
        public ICollection<User> DislikeReactionUsers { get; set; } = default!;
        public User Poster { get; set; } = default!;
        public IEnumerable<Comment> Comments { get; set; } = default!;
        public IEnumerable<Report> Reports { get; set; } = default!;
    }
}

namespace Database.Entities
{
    public class Comment : BaseEntity
    {
        public Guid UserId { get; set; } = default!;
        public Guid PostId { get; set; } = default!;
        public String Text { get; set; } = default!;

        //Entities tha are referenced by the foreign keys
        public User Poster { get; set; } = default!;
        public Post Post { get; set; } = default!;
    }
}

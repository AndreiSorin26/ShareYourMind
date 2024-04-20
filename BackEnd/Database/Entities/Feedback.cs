namespace Database.Entities
{
    public class Feedback : BaseEntity
    {
        public Guid UserId { get; set; } = default!;
        public string Text { get; set; } = default!;
        public float UIRating { get; set; } = default;
        public float DataFlowRating { get; set; } = default;
        public float UXRating { get; set; } = default;
        public float CommunityRating { get; set; } = default;

        //Entities tha are referenced by the foreign keys
        public User User { get; set; } = default!;
    }
}

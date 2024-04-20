namespace Services.DTOs
{
    public class NewReactionDTO
    {
        public Guid PostId { get; set; } = default!;
        public string ReactionType { get; set; } = default!;
    }
}

namespace Services.DTOs.EntityMappings
{
    public class PostDTO : BaseEntityDTO
    {
        public string PosterDisplayName { get; set; } = default!;
        public string Text { get; set; } = default!;
        public int LoveReactionCount { get; set; }
        public int LaughReactionCount { get; set; }
        public int DislikeReactionCount { get; set; }
    }
}

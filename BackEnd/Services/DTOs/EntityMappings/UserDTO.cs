namespace Services.DTOs.EntityMappings
{
    public class UserDTO : BaseEntityDTO
    {
        public string Username { get; set; } = default!;
        public string Tag { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string Role { get; set; } = default!;
        public IEnumerable<PostDTO> LoveReactionPosts { get; set; } = default!;
        public IEnumerable<PostDTO> LaughReactionPosts { get; set; } = default!;
        public IEnumerable<PostDTO> DislikeReactionPosts { get; set; } = default!;
        public FeedbackDTO Feedback { get; set; } = default!;
    }
}

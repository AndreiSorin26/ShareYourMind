namespace Services.DTOs.EntityMappings
{
    public class CommentDTO : BaseEntityDTO
    {
        public String Text { get; set; } = default!;
        public String PosterDisplayName { get; set; } = default!;
    }
}

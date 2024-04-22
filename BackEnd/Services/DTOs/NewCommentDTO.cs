namespace Services.DTOs
{
    public class NewCommentDTO
    {
        public Guid PostId { get; set; } = default!;
        public String Text { get; set; } = default!;
    }
}

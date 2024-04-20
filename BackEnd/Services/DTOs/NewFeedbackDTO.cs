using Services.DTOs.EntityMappings;

namespace Services.DTOs
{
    public class NewFeedbackDTO : BaseEntityDTO
    {
        public string Text { get; set; } = default!;
        public float UIRating { get; set; } = default!;
        public float DataFlowRating { get; set; } = default!;
        public float UXRating { get; set; } = default!;
        public float CommunityRating { get; set; } = default!;
    }
}

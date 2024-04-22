namespace Services.DTOs
{
    public class FeedbackMetric
    {
        public String Name { get; set; } = default!;
        public float Value { get; set; } = default!;
        public String Type { get; set; } = default!;
        public String? Text { get; set; } = default;
    }
}

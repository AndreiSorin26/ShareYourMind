namespace Database.Entities
{
    public class Report : BaseEntity
    {
        public Guid ReportingUserId { get; set; } = default!;
        public Guid PostId { get; set; } = default!;
        public String Text { get; set; } = default!;
        public bool Closed { get; set; } = default!;

        //Entities tha are referenced by the foreign keys
        public User Reporter { get; set; } = default!;
        public Post Post { get; set; } = default!;
    }
}

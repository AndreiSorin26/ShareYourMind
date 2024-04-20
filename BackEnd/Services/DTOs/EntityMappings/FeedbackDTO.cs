using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.EntityMappings
{
    public class FeedbackDTO : BaseEntityDTO
    {
        public String UserDisplayName { get; set; } = default!;
        public string Text { get; set; } = default!;
        public float UIRating { get; set; } = default!;
        public float DataFlowRating { get; set; } = default!;
        public float UXRating { get; set; } = default!;
        public float CommunityRating { get; set; } = default!;
    }
}

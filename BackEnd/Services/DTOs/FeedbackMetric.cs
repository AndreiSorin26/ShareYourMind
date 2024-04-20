using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

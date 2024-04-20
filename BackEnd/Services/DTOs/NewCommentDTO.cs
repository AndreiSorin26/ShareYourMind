using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public class NewCommentDTO
    {
        public Guid PostId { get; set; } = default!;
        public String Text { get; set; } = default!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.EntityMappings
{
    public class CommentDTO : BaseEntityDTO
    {
        public String Text { get; set; } = default!;
        public String PosterDisplayName { get; set; } = default!;
    }
}

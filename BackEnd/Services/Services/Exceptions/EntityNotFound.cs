using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public class EntityNotFound : Exception
    {
        public EntityNotFound() { }

        public EntityNotFound(string message)
            : base(message) { }

        public EntityNotFound(string message, Exception inner)
            : base(message, inner) { }
    }
}

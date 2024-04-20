using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public class BadQuery : Exception
    {
        public BadQuery() { }

        public BadQuery(string message)
            : base(message) { }

        public BadQuery(string message, Exception inner)
            : base(message, inner) { }
    }
}

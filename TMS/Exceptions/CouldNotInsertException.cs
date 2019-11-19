using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Exceptions
{
    public class CouldNotInsertException : Exception
    {
        public CouldNotInsertException()
        {
        }

        public CouldNotInsertException(string message) : base(message)
        {
        }

        public CouldNotInsertException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Exceptions
{
    public class CouldNotGetIdException : Exception
    {
        public CouldNotGetIdException()
        {
        }

        public CouldNotGetIdException(string message) : base(message)
        {
        }

        public CouldNotGetIdException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

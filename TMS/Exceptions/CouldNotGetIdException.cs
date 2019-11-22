using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Exceptions
{
    /// <summary>
    /// This exception is thrown when access to the last inserted ID is attempted, but failed.
    /// </summary>
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

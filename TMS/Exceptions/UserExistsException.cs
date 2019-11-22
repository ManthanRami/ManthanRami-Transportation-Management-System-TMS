using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Exceptions
{
    /// <summary>
    /// This exception is thrown when creation of a non-unique user is attempted.
    /// </summary>
    public class UserExistsException : Exception
    {
        public UserExistsException()
        {
        }

        public UserExistsException(string message) : base(message)
        {
        }

        public UserExistsException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

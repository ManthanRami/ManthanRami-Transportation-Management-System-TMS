using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Exceptions
{
    /// <summary>
    /// This exception is thrown when access to a non-existent user is attempted.
    /// </summary>
    public class UserNotExistsException : Exception
    {
        public UserNotExistsException()
        {
        }

        public UserNotExistsException(string message) : base(message)
        {
        }

        public UserNotExistsException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

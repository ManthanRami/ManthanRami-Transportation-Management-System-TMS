using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Exceptions
{
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

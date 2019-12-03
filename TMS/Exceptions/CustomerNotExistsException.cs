using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Exceptions
{
    public class CustomerNotExistsException : Exception
    {
        public CustomerNotExistsException()
        {
        }

        public CustomerNotExistsException(string message) : base(message)
        {
        }

        public CustomerNotExistsException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

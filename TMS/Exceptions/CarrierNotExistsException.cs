using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Exceptions
{
    public class CarrierNotExistsException : Exception
    {
        public CarrierNotExistsException()
        {
        }

        public CarrierNotExistsException(string message) : base(message)
        {
        }

        public CarrierNotExistsException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

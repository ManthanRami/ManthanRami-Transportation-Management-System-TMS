using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Exceptions
{
    /// <summary>
    /// This exception is thrown when access is attempted to a carrier that doesn't exist
    /// </summary>
    public class CouldNotUpdateException : Exception
    {
        public CouldNotUpdateException()
        {
        }

        public CouldNotUpdateException(string message) : base(message)
        {
        }

        public CouldNotUpdateException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
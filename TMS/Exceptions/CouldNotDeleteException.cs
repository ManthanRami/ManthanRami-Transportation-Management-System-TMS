using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Exceptions
{
    public class CouldNotDeleteException : Exception
    {
        public CouldNotDeleteException()
        {
        }

        public CouldNotDeleteException(string message) : base(message)
        {
        }

        public CouldNotDeleteException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Exceptions
{
    public class CouldNotFindRateException : Exception
    {
        public CouldNotFindRateException()
        {
        }

        public CouldNotFindRateException(string message) : base(message)
        {
        }

        public CouldNotFindRateException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

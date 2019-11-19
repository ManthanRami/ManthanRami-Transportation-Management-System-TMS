﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Exceptions
{
    class UserNotExistException : Exception
    {
        public UserNotExistException()
        {
        }

        public UserNotExistException(string message) : base(message)
        {
        }

        public UserNotExistException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

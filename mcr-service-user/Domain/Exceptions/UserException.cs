﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr_service_user.Domain.Exceptions
{
    public class UserException : Exception
    {
        public UserException()
        {
        }

        public UserException(string message)
            : base(message)
        {
        }

        public UserException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

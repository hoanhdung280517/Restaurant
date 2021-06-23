using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Utilities.Exceptions
{
    public class RSException : Exception
    {
        public RSException()
        {
        }

        public RSException(string message)
            : base(message)
        {
        }

        public RSException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Domain
{
    public class SecondContextDomainException : Exception
    {
        public SecondContextDomainException(string message) : base(message)
        {

        }
    }
}

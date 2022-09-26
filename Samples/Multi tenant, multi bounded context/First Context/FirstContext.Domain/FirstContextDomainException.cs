using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Domain
{
    public class FirstContextDomainException : Exception
    {
        public FirstContextDomainException(string message) : base(message)
        {

        }
    }
}

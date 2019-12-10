using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSException
{
    public class ProductException : ApplicationException
    {
        public ProductException() : base() { }

        public ProductException(string message) : base(message) { }

        public ProductException(string message, Exception innerException) : base(message, innerException) { }
    }
}


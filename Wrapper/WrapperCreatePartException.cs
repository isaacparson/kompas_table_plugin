using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrapperLib
{
    public class WrapperCreatePartException : Exception
    {
        public WrapperCreatePartException(string message) : base(message) { }
    }
}

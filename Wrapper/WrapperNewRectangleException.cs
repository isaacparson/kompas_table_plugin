using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrapperLib
{
    public class WrapperNewRectangleException : Exception
    {
        public WrapperNewRectangleException(string message) : base(message) { }
    }
}

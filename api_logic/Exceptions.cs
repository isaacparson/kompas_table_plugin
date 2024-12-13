using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_logic
{
    public class WrapperOpenCadException : Exception
    {
        public WrapperOpenCadException(string message) : base(message) { }
    }
    public class WrapperCreatePartException : Exception
    {
        public WrapperCreatePartException(string message) : base(message) { }
    }
    public class WrapperNewRectangleException : Exception
    {
        public WrapperNewRectangleException(string message) : base(message) { }
    }
    public class WrapperExtrudeException : Exception
    {
        public WrapperExtrudeException(string message) : base(message) { }
    }
}

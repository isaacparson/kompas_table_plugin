using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrapperLib
{
    public class WrapperOpenCadException : Exception
    {
        public WrapperOpenCadException(string message) : base(message) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    /// <summary>
    /// Исключение создания детали
    /// </summary>
    public class ParameterOutOfRangeException : Exception
    {
        public ParameterOutOfRangeException(string message) : base(message) { }
    }
}

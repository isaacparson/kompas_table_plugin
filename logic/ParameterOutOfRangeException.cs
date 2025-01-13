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
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        public ParameterOutOfRangeException(string message) : base(message) { }
    }
}

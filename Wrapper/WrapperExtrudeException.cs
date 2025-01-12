using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrapperLib
{
    /// <summary>
    /// Исключение выдавливания
    /// </summary>
    public class WrapperExtrudeException : Exception
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        public WrapperExtrudeException(string message) : base(message) { }
    }
}

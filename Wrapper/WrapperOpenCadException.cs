﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrapperLib
{
    /// <summary>
    /// Исключение открытия САПР
    /// </summary>
    public class WrapperOpenCadException : Exception
    {
        public WrapperOpenCadException(string message) : base(message) { }
    }
}

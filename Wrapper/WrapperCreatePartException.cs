﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrapperLib
{
    /// <summary>
    /// Исключение создания детали
    /// </summary>
    public class WrapperCreatePartException : Exception
    {
        public WrapperCreatePartException(string message) : base(message) { }
    }
}

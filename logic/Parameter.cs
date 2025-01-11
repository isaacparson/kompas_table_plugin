using System.CodeDom;

namespace ParametersLogic
{
    /// <summary>
    /// Параметр, используемый для передачи размеров при построении модели в САПР
    /// </summary>
    public class Parameter
    {
        int _value;
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="value">Числовое значение параметра</param>
        public Parameter(int value, int minValue, int maxValue)
        {
            _value = value;
            if (value < minValue || value > maxValue)
            {
                throw new System.Exception("[" + minValue + ";" + maxValue + "]");
            }
        }

        /// <summary>
        /// Максимальное значение параметра.
        /// </summary>
        public int MaxValue { get; }

        /// <summary>
        /// Минимальное значение параметра.
        /// </summary>
        public int MinValue { get; }

        /// <summary>
        /// Значение параметра.
        /// </summary>
        public int Value 
        { 
            get => _value;
        }
    }
}

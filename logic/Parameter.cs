namespace ParametersLogic
{
    /// <summary>
    /// Параметр, используемый для передачи размеров при построении модели в САПР
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="value">Числовое значение параметра</param>
        public Parameter(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Максимальное значение параметра
        /// </summary>
        public int MaxValue { get; set; }

        /// <summary>
        /// Минимальное значение параметра
        /// </summary>
        public int MinValue { get; set; }

        /// <summary>
        /// Значение параметра
        /// </summary>
        public int Value { get; set; }
    }
}

namespace ParametersLogic
{
    public class Parameter
    {
        public int value_;
        public readonly int maxValue_;
        public readonly int minValue_;

        public int MaxValue
        {
            get
            {
                return maxValue_;
            }
        }

        public int MinValue
        {
            get
            {
                return minValue_;
            }
        }

        public int Value
        {
            get 
            { 
                return value_; 
            }
            set 
            {
                value_ = value;
            }
        }

        public Parameter(int maxValue, int minValue, int value)
        {
            maxValue_ = maxValue;
            minValue_ = minValue;
            Value = value;
        }
    }
}

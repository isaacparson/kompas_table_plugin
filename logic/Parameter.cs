namespace ParametersLogic
{
    public class Parameter
    {
        public int value_;
        public readonly int maxValue_;
        public readonly int minValue_;

        private Parameter(int maxValue, int minValue, int value)
        {
            maxValue_ = maxValue;
            minValue_ = minValue;
            Value = value;
        }

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

        public static Parameter CreateInstance(ParamType type, int value)
        {
            switch (type)
            {
                case ParamType.TopWidth:
                    {
                        return new Parameter(5000, 500, value);
                    }
                case ParamType.TopDepth:
                    {
                        return new Parameter(5000, 500, value);
                    }
                case ParamType.TopHeight:
                    {
                        return new Parameter(100, 16, value);
                    }
                case ParamType.LegWidth:
                    {
                        return new Parameter(200, 20, value);
                    }
                case ParamType.TableHeight:
                    {
                        return new Parameter(1400, 500, value);
                    }
                default:
                    {
                        return new Parameter(0, 0, value);
                    }
            }
        }
    }
}

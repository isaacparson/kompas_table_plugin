using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    internal class Parameter
    {
        public int value_;
        public int maxValue_;
        public int minValue_;

        public int Value
        {
            get 
            { 
                return value_; 
            }
            set 
            {
                if (!Validate(value))
                {
                    throw new Exception();
                }
                value_ = value;
            }
        }

        public Parameter(int maxValue, int minValue, int value)
        {
            maxValue_ = maxValue;
            minValue_ = minValue;
            Value = value;
        }

        public bool Validate(int value)
        {
            return value < maxValue_ && value > minValue_;
        }

    }
}

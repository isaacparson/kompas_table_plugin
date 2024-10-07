using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    using Dict = Dictionary<ParamType, Parameter>;

    enum ParamType
    {
        TopWidth,
        TopDepth,
        TopHeight,
        LegWidth,
        LegDepth,
        TableHeight,
    }

    internal class Parameters
    {
        public Dict params_;

        public Dict Params
        {
            get 
            { 
                return params_; 
            }
            set 
            { 
                params_ = value; 
            }
        }

        public Parameters(Dict parameters)
        {
            Params = parameters;
        }

        public void Validate(ParamType type, Parameter parameter )
        {
            var value = parameter.Value;

            switch (type)
            {
                case ParamType.TopWidth:
                    {
                        if (value < 500 && value > 5000)
                        {
                            throw new Exception();
                        }
                    }
                    break;
                case ParamType.TopDepth:
                    {
                        if (value < 500 && value > 5000)
                        {
                            throw new Exception();
                        }
                    }
                    break;
                case ParamType.TopHeight:
                    {
                        if (value < 16 && value > 100)
                        {
                            throw new Exception();
                        }
                    }
                    break;
                case ParamType.LegWidth:
                    {
                        if (value < 20 && value > 200)
                        {
                            throw new Exception();
                        }
                    }
                    break;
                case ParamType.LegDepth:
                    {
                        if (value < 20 && value > 200)
                        {
                            throw new Exception();
                        }
                    }
                    break;
                case ParamType.TableHeight:
                    {
                        if (value < 500 && value > 1400)
                        {
                            throw new Exception();
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}

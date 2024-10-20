using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    using Dict = Dictionary<ParamType, Parameter>;

    public enum ParamType
    {
        TopWidth,
        TopDepth,
        TopHeight,
        LegWidth,
        LegDepth,
        TableHeight,
    }

    public enum DependentParameters
    {    
        TopAndLegsArea,
    }

    public class Parameters
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

        /// <summary>
        /// Возвращает пустой список, если валидация прошла успешно. 
        /// Иначе возвращает список зависисмых параметров, которые имеют неверные значения.
        /// </summary>
        /// <returns></returns>
        public List<DependentParameters> Validate()
        {
            Parameter topWidth;
            Parameter topDepth;
            Parameter legWidth;
            params_.TryGetValue(ParamType.TopWidth, out topWidth);
            params_.TryGetValue(ParamType.TopDepth, out topDepth);
            params_.TryGetValue(ParamType.LegWidth, out legWidth);

            int twoLegsWidth = legWidth.Value * 2 + 200;

            var incorrect = new List<DependentParameters>();

            if (topWidth.Value < twoLegsWidth || topDepth.Value < twoLegsWidth)
            {
                incorrect.Add(DependentParameters.TopAndLegsArea);
            }

            return incorrect;
        }
    }
}

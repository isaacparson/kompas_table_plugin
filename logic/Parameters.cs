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

    public enum IncorrectParameters
    {    
        TopWidthIncorrect,
        TopDepthIncorrect,
        TopHeightIncorrect,
        LegWidthIncorrect,
        LegDepthIncorrect,
        TableHeightIncorrect,
        TopAndLegsAreaIncorrect,
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
        public List<IncorrectParameters> Validate()
        {
            Parameter topWidth;
            Parameter topDepth;
            Parameter topHeight;
            Parameter legWidth;
            Parameter tableHeight;
            params_.TryGetValue(ParamType.TopWidth, out topWidth);
            params_.TryGetValue(ParamType.TopDepth, out topDepth);
            params_.TryGetValue(ParamType.TopHeight, out topHeight);   
            params_.TryGetValue(ParamType.LegWidth, out legWidth);
            params_.TryGetValue(ParamType.TableHeight, out tableHeight);

            int twoLegsWidth = legWidth.Value * 2 + 200;

            var incorrect = new List<IncorrectParameters>();

            if (topWidth.Value < topWidth.MinValue || topWidth.Value > topWidth.MaxValue)
            {
                incorrect.Add(IncorrectParameters.TopWidthIncorrect);
            }
            if (topDepth.Value < topDepth.MinValue || topDepth.Value > topDepth.MaxValue)
            {
                incorrect.Add(IncorrectParameters.TopDepthIncorrect);
            }
            if (topHeight.Value < topHeight.MinValue || topHeight.Value > topHeight.MaxValue)
            {
                incorrect.Add(IncorrectParameters.TopHeightIncorrect);
            }
            if (legWidth.Value < legWidth.MinValue || legWidth.Value > legWidth.MaxValue)
            {
                incorrect.Add(IncorrectParameters.LegWidthIncorrect);
            }
            if (tableHeight.Value < tableHeight.MinValue || tableHeight.Value > tableHeight.MaxValue)
            {
                incorrect.Add(IncorrectParameters.TableHeightIncorrect);
            }
            if (topWidth.Value < twoLegsWidth || topDepth.Value < twoLegsWidth)
            {
                incorrect.Add(IncorrectParameters.TopAndLegsAreaIncorrect);
            }

            return incorrect;
        }
    }
}

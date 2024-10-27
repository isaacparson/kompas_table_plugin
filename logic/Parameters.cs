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
        public Dict _parameters;

        public Dict Params
        {
            get 
            { 
                return _parameters; 
            }
            set 
            {
                _parameters = value; 
            }
        }

        public Parameters()
        {
        }

        public List<IncorrectParameters> SetParameters(Dict parameters)
        {
            var incorrect = Validate(parameters);
            if (incorrect.Count == 0)
            {
                _parameters = parameters;
            }
            return incorrect;
        }

        public Dict GetParameters()
        {
            return _parameters;
        }

        /// <summary>
        /// Возвращает пустой список, если валидация прошла успешно. 
        /// Иначе возвращает список параметров, которые имеют неверные значения.
        /// </summary>
        /// <returns></returns>
        private List<IncorrectParameters> Validate(Dict parameters)
        {
            Parameter topWidth;
            Parameter topDepth;
            Parameter topHeight;
            Parameter legWidth;
            Parameter tableHeight;
            parameters.TryGetValue(ParamType.TopWidth, out topWidth);
            parameters.TryGetValue(ParamType.TopDepth, out topDepth);
            parameters.TryGetValue(ParamType.TopHeight, out topHeight);   
            parameters.TryGetValue(ParamType.LegWidth, out legWidth);
            parameters.TryGetValue(ParamType.TableHeight, out tableHeight);

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

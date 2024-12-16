using System.Collections.Generic;

namespace ParametersLogic
{

    public class Parameters
    {
        public Dictionary<ParamType, int> _parameters;

        public Dictionary<ParamType, int> Params
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

        public List<IncorrectParameters> SetParameters(Dictionary<ParamType, int> parameters)
        {
            var incorrect = Validate(parameters);
            if (incorrect.Count == 0)
            {
                _parameters = parameters;
            }
            return incorrect;
        }

        public Dictionary<ParamType, int> GetParameters()
        {
            return _parameters;
        }

        /// <summary>
        /// Возвращает пустой список, если валидация прошла успешно. 
        /// Иначе возвращает список параметров, которые имеют неверные значения.
        /// </summary>
        private List<IncorrectParameters> Validate(Dictionary<ParamType, int> parameters)
        {
            parameters.TryGetValue(ParamType.TopWidth, out int topWidth);
            parameters.TryGetValue(ParamType.TopDepth, out int topDepth);
            parameters.TryGetValue(ParamType.TopHeight, out int topHeight);   
            parameters.TryGetValue(ParamType.LegWidth, out int legWidth);
            parameters.TryGetValue(ParamType.TableHeight, out int tableHeight);

            int twoLegsWidth = legWidth * 2 + 200;

            var incorrect = new List<IncorrectParameters>();

            if (topWidth < 500 || topWidth > 5000)
            {
                incorrect.Add(IncorrectParameters.TopWidthIncorrect);
            }
            if (topDepth < 500 || topDepth > 5000)
            {
                incorrect.Add(IncorrectParameters.TopDepthIncorrect);
            }
            if (topHeight < 16 || topHeight > 100)
            {
                incorrect.Add(IncorrectParameters.TopHeightIncorrect);
            }
            if (legWidth < 20 || legWidth > 200)
            {
                incorrect.Add(IncorrectParameters.LegWidthIncorrect);
            }
            if (tableHeight < 500 || tableHeight > 1400)
            {
                incorrect.Add(IncorrectParameters.TableHeightIncorrect);
            }
            if (topWidth < twoLegsWidth || topDepth < twoLegsWidth)
            {
                incorrect.Add(IncorrectParameters.TopAndLegsAreaIncorrect);
            }

            return incorrect;
        }
    }
}

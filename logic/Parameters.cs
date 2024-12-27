using System.Collections.Generic;

namespace ParametersLogic
{

    public class Parameters
    {
        public Dictionary<ParamType, Parameter> Params { get; set; }

        public List<IncorrectParameters> SetParameters(Dictionary<ParamType, Parameter> parameters)
        {
            var incorrect = Validate(parameters);
            Params = parameters;
            return incorrect;
        }

        public Dictionary<ParamType, Parameter> GetParameters()
        {
            return Params;
        }

        /// <summary>
        /// Возвращает пустой список, если валидация прошла успешно. 
        /// Иначе возвращает список параметров, которые имеют неверные значения.
        /// </summary>
        private List<IncorrectParameters> Validate(Dictionary<ParamType, Parameter> parameters)
        {
            var incorrect = new List<IncorrectParameters>();

            var legWidth = parameters[ParamType.LegWidth];
            var topWidth = parameters[ParamType.TopWidth];
            var topDepth = parameters[ParamType.TopDepth];

            int twoLegsWidth = legWidth.Value * 2 + 200;
            bool wereIncorrect = false;

            foreach (var parameter in parameters)
            {
                var value = parameter.Value;
                if (value.Value < value.MinValue || value.Value > value.MaxValue)
                {
                    switch(parameter.Key)
                    {
                        case (ParamType.TopWidth):
                        {
                            incorrect.Add(IncorrectParameters.TopWidthIncorrect);
                            break;
                        }
                        case (ParamType.TopDepth):
                        {
                            incorrect.Add(IncorrectParameters.TopDepthIncorrect);
                            break;
                        }
                        case (ParamType.LegWidth):
                        {
                            incorrect.Add(IncorrectParameters.LegWidthIncorrect);
                            break;
                        }
                        case (ParamType.TableHeight):
                        {
                            incorrect.Add(IncorrectParameters.TableHeightIncorrect);
                            break;
                        }
                        case (ParamType.TopHeight):
                        {
                            incorrect.Add(IncorrectParameters.TopHeightIncorrect);
                            break;
                        }
                    }
                }
            }
            if ((topWidth.Value < twoLegsWidth || topDepth.Value < twoLegsWidth) && !wereIncorrect)
            {
                incorrect.Add(IncorrectParameters.TopAndLegsAreaIncorrect);
                wereIncorrect = true;
            }
            return incorrect;
        }
    }
}

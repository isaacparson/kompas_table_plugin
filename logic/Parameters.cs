using logic;
using System;
using System.Collections.Generic;

namespace ParametersLogic
{
    /// <summary>
    /// Набор параметров, реализующий валидацию их значений.
    /// </summary>
    public class Parameters
    {
        /// <summary>
        /// Словарь параметров.
        /// </summary>
        public Dictionary<ParamType, Parameter> Params { get; set; }

        /// <summary>
        /// Установить параметры и провалидировать их значения.
        /// </summary>
        /// <param name="parameters">Словарь параметров.</param>
        /// <returns>Список параметров, непрошедших валидацию.</returns>
        public Dictionary<IncorrectParameters, string> SetParameters(Dictionary<ParamType, int> parameters)
        {
            Dictionary<ParamType, Parameter> resultParameters = new Dictionary<ParamType, Parameter>();
            var incorrect = Validate(parameters, resultParameters);
            Params = resultParameters;
            return incorrect;
        }

        /// <summary>
        /// Получить параметры.
        /// </summary>
        /// <returns>Словарь параметров.</returns>
        public Dictionary<ParamType, Parameter> GetParameters()
        {
            return Params;
        }

        /// <summary>
        /// Провести валидацию переданных параметров.
        /// </summary>
        /// <param name="parameters">Словарь параметров.</param>
        /// <returns>Список параметров, непрошедших валидацию.
        /// Пустой список если все параметры корректны.</returns>
        private Dictionary<IncorrectParameters, string> Validate(
            Dictionary<ParamType, int> parameters, 
            Dictionary<ParamType, Parameter> resultParameters)
        {
            var incorrect = new Dictionary<IncorrectParameters, string>();

            var legWidth = parameters[ParamType.LegWidth];
            var topWidth = parameters[ParamType.TopWidth];
            var topDepth = parameters[ParamType.TopDepth];
            var topHeight = parameters[ParamType.TopHeight];
            var tableHeight = parameters[ParamType.TableHeight];
            int twoLegsWidth = legWidth * 2 + 200;

            var minMaxValues = new Dictionary<ParamType, Tuple<int, int>>
            {
                { ParamType.TopWidth, new Tuple<int, int>(500, 5000) },
                { ParamType.TopDepth, new Tuple<int, int>(500, 5000) },
                { ParamType.TopHeight, new Tuple<int, int>(16, 100) },
                { ParamType.LegWidth, new Tuple<int, int>(20, 200) },
                { ParamType.TableHeight, new Tuple<int, int>(500, 1400) },
            };

            bool wereIncorrect = false;

            foreach(var parameter in parameters)
            {
                var minValue = minMaxValues[parameter.Key].Item1;
                var maxValue = minMaxValues[parameter.Key].Item2;
                try
                {
                    Parameter newParameter = new Parameter(parameter.Value, minValue, maxValue);
                }
                catch(ParameterOutOfRangeException ex)
                {
                    var incorrectType = GetIncorrectParameter(parameter.Key);
                    incorrect.Add(incorrectType, ex.Message);
                    wereIncorrect = true;
                }
            }

            if ((topWidth < twoLegsWidth || topDepth < twoLegsWidth) 
                && !wereIncorrect)
            {
                incorrect.Add(IncorrectParameters.TopAndLegsAreaIncorrect, "");
                wereIncorrect = true;
            }
            return incorrect;
        }

        /// <summary>
        /// Получить тип некорректного параметра, который относится к определенному типу параметра.
        /// </summary>
        /// <param name="type">Тип параметра.</param>
        /// <returns></returns>
        private IncorrectParameters GetIncorrectParameter(ParamType type)
        {
            switch(type)
            {
                case ParamType.TopWidth:
                {
                    return IncorrectParameters.TopWidthIncorrect;
                    break;
                }
                case ParamType.TopDepth:
                {
                    return IncorrectParameters.TopDepthIncorrect;
                    break;
                }
                case ParamType.TopHeight:
                {
                    return IncorrectParameters.TopHeightIncorrect;
                    break;
                }
                case ParamType.LegWidth:
                {
                    return IncorrectParameters.LegWidthIncorrect;
                    break;
                }
                case ParamType.TableHeight:
                {
                    return IncorrectParameters.TableHeightIncorrect;
                    break;
                }
                default:
                {
                    return IncorrectParameters.TopAndLegsAreaIncorrect;
                }
            }
        }
    }
}

using logic;
using System;
using System.Collections.Generic;

namespace ParametersLogic
{
    /// <summary>
    /// Набор параметров, реализующий валидацию их значений
    /// </summary>
    public class Parameters
    {
        /// <summary>
        /// Словарь параметров
        /// </summary>
        public Dictionary<ParamType, Parameter> Params { get; set; }

        /// <summary>
        /// Установить параметры и провалидировать их значения
        /// </summary>
        /// <param name="parameters">Словарь параметров</param>
        /// <returns>Список параметров, непрошедших валидацию</returns>
        public Dictionary<IncorrectParameters, string> SetParameters(Dictionary<ParamType, int> parameters)
        {
            Dictionary<ParamType, Parameter> resultParameters = new Dictionary<ParamType, Parameter>();
            var incorrect = Validate(parameters, resultParameters);
            Params = resultParameters;
            return incorrect;
        }

        /// <summary>
        /// Получить параметры
        /// </summary>
        /// <returns>Словарь параметров</returns>
        public Dictionary<ParamType, Parameter> GetParameters()
        {
            return Params;
        }

        /// <summary>
        /// Провести валидацию переданных параметров
        /// </summary>
        /// <param name="parameters">Словарь параметров</param>
        /// <returns>Список параметров, непрошедших валидацию.
        /// Пустой список если все параметры корректны</returns>
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

            bool wereIncorrect = false;

            try
            {
                Parameter topWidthParameter = new Parameter(topWidth, 500, 5000);
                resultParameters.Add(ParamType.TopWidth, topWidthParameter);
            }
            catch(ParameterOutOfRangeException ex)
            {
                incorrect.Add(IncorrectParameters.TopWidthIncorrect, ex.Message);
                wereIncorrect = true;
            }
            try
            {
                Parameter topDepthParameter = new Parameter(topDepth, 500, 5000);
                resultParameters.Add(ParamType.TopDepth, topDepthParameter);
            }
            catch (ParameterOutOfRangeException ex)
            {
                incorrect.Add(IncorrectParameters.TopDepthIncorrect, ex.Message);
                wereIncorrect = true;
            }
            try
            {
                Parameter topHeightParameter = new Parameter(topHeight, 16, 100);
                resultParameters.Add(ParamType.TopHeight, topHeightParameter);
            }
            catch (ParameterOutOfRangeException ex)
            {
                incorrect.Add(IncorrectParameters.TopHeightIncorrect, ex.Message);
                wereIncorrect = true;
            }
            try
            {
                Parameter legWidthParameter = new Parameter(legWidth, 20, 200);
                resultParameters.Add(ParamType.LegWidth, legWidthParameter);
            }
            catch (ParameterOutOfRangeException ex)
            {
                incorrect.Add(IncorrectParameters.LegWidthIncorrect, ex.Message);
                wereIncorrect = true;
            }
            try
            {
                Parameter tableHeightParameter = new Parameter(tableHeight, 500, 1400);
                resultParameters.Add(ParamType.TableHeight, tableHeightParameter);
            }
            catch (ParameterOutOfRangeException ex)
            {
                incorrect.Add(IncorrectParameters.TableHeightIncorrect, ex.Message);
                wereIncorrect = true;
            }

            if ((topWidth < twoLegsWidth || topDepth < twoLegsWidth) 
                && !wereIncorrect)
            {
                incorrect.Add(IncorrectParameters.TopAndLegsAreaIncorrect, "");
                wereIncorrect = true;
            }
            return incorrect;
        }
    }
}

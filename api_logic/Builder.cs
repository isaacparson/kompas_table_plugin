using ParametersLogic;
using System.Diagnostics;
using WrapperLib;

namespace ApiLogic
{
    public class Builder
    {
        private IWrapper _wrapper;
        private Parameters _parameters;
        private static int counter = 0;

        public Builder(Parameters parameters, Cad cad)
        {
            WrapperFactory factroy = new WrapperFactory();
            _wrapper = factroy.MakeWrapper(cad);
            _parameters = parameters;
        }

        public void Build()
        {
            if (!_wrapper.IsCadRunning() || counter == 23)
            {
                _wrapper.OpenCad();
            }
            _wrapper.CreatePart();
            counter++;

            var topWidth = _parameters.Params[ParamType.TopWidth].Value;
            var topDepth = _parameters.Params[ParamType.TopDepth].Value;
            var topHeight = _parameters.Params[ParamType.TopHeight].Value;
            var legWidth = _parameters.Params[ParamType.LegWidth].Value;
            var tableHeight = _parameters.Params[ParamType.TableHeight].Value;
            int legHeight = tableHeight - topHeight;
            double originX = 0;
            double originY = 0;

            string topName = "Top";
            _wrapper.NewRectangle(originX, originY, topWidth, topDepth, topName);
            _wrapper.Extrude(topHeight, topName, true);

            string[] legsNames = { "Leg1", "Leg2", "Leg3", "Leg4" };
            _wrapper.NewRectangle(originX,
                                  originY,
                                  legWidth,
                                  legWidth,
                                  legsNames[0]);
            _wrapper.Extrude(legHeight, legsNames[0], false);

            _wrapper.NewRectangle(topWidth - legWidth,
                                  originY,
                                  legWidth,
                                  legWidth,
                                  legsNames[1]);
            _wrapper.Extrude(legHeight, legsNames[1], false);

            _wrapper.NewRectangle(originX,
                                  topDepth - legWidth,
                                  legWidth,
                                  legWidth,
                                  legsNames[2]);
            _wrapper.Extrude(legHeight, legsNames[2], false);

            _wrapper.NewRectangle(topWidth - legWidth,
                                  topDepth - legWidth,
                                  legWidth,
                                  legWidth,
                                  legsNames[3]);
            _wrapper.Extrude(legHeight, legsNames[3], false);
        }
    }
}

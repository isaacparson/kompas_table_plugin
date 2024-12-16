using ParametersLogic;
using WrapperLib;

namespace ApiLogic
{
    public class Builder
    {
        private IWrapper _wrapper;
        private Parameters _parameters;

        public Builder(Parameters parameters, Cad cad)
        {
            WrapperFactory factroy = new WrapperFactory();
            _wrapper = factroy.MakeWrapper(cad);
            _parameters = parameters;
        }

        public void Build()
        {
            _wrapper.OpenCad();
            _wrapper.CreatePart();

            _parameters.Params.TryGetValue(ParamType.TopWidth, out int topWidth);
            _parameters.Params.TryGetValue(ParamType.TopDepth, out int topDepth);
            _parameters.Params.TryGetValue(ParamType.TopHeight, out int topHeight);
            _parameters.Params.TryGetValue(ParamType.LegWidth, out int legWidth);
            _parameters.Params.TryGetValue(ParamType.TableHeight, out int tableHeight);
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

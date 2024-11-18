using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using api_logic;

namespace logic
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

            _parameters.Params.TryGetValue(ParamType.TopWidth, out Parameter topWidth);
            _parameters.Params.TryGetValue(ParamType.TopDepth, out Parameter topDepth);
            _parameters.Params.TryGetValue(ParamType.TopHeight, out Parameter topHeight);
            _parameters.Params.TryGetValue(ParamType.LegWidth, out Parameter legWidth);
            _parameters.Params.TryGetValue(ParamType.TableHeight, out Parameter tableHeight);
            int legHeight = tableHeight.Value - topHeight.Value;
            double originX = 0;
            double originY = 0;

            string topName = "Столешница";
            _wrapper.NewRectangle(originX, originY, topWidth.Value, topDepth.Value, topName);
            _wrapper.Extrude(topHeight.Value, topName);

            string[] legsNames = { "Ножка1", "Ножка2", "Ножка3", "Ножка4" };
            _wrapper.NewRectangle(originX, 
                                  originY, 
                                  legWidth.Value, 
                                  legWidth.Value,
                                  legsNames[0]);
            _wrapper.Extrude(-legHeight, legsNames[0]);

            _wrapper.NewRectangle(topWidth.Value - legWidth.Value, 
                                  originY, 
                                  legWidth.Value, 
                                  legWidth.Value,
                                  legsNames[1]);
            _wrapper.Extrude(-legHeight, legsNames[1]);

            _wrapper.NewRectangle(originX, 
                                  topDepth.Value - legWidth.Value, 
                                  legWidth.Value, 
                                  legWidth.Value,
                                  legsNames[2]);
            _wrapper.Extrude(-legHeight, legsNames[2]);

            _wrapper.NewRectangle(topWidth.Value - legWidth.Value, 
                                  topDepth.Value - legWidth.Value, 
                                  legWidth.Value, 
                                  legWidth.Value,
                                  legsNames[3]);
            _wrapper.Extrude(-legHeight, legsNames[3]);
        }
    }
}

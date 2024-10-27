using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
            Prepare();
            //BuildTop();
            //BuildLegs();
        }

        private void Prepare()
        {
            _wrapper.OpenCad();
        }

        private void BuildTop()
        {
            _wrapper.CreateRectangle();
            _wrapper.Extrude();
        }

        private void BuildLegs()
        {
            // 4 вызова, может сделать массивом или отражением?
        }
    }
}

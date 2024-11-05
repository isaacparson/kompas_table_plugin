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
            _wrapper.OpenCad();
            _wrapper.CreatePart(_parameters);
        }
    }
}

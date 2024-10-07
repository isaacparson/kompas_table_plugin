using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using api_logic;

namespace logic
{
    internal class Builder
    {
        private IWrapper _wrapper;
        void Build(Parameters parameters, Cad cad)
        {
            switch (cad)
            {
                case Cad.Kompas:
                    {
                        _wrapper = new KompasWrapper();
                    }
                    break;
                case Cad.Inventor:
                    {
                        _wrapper = new InventorWrapper();
                    }
                    break;
                default:
                    break;
            }
            Prepare();
            BuildTop();
            BuildLegs();
        }

        void Prepare()
        {
            _wrapper.OpenCad();
        }

        void BuildTop()
        {
            _wrapper.CreateRectangle();
            _wrapper.Extrude();
        }

        void BuildLegs()
        {
            // 4 вызова, может сделать массивом или отражением?
        }



    }
}

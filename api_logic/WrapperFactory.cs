using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_logic
{
    public class WrapperFactory
    {
        public IWrapper MakeWrapper(Cad cad)
        {
            switch (cad)
            {
                case Cad.Kompas: return new KompasWrapper();
                case Cad.AutoCad: return new InventorWrapper();
                default: return new KompasWrapper();
            }
        }
    }
}

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
            if (cad == Cad.Kompas)
            {
                return new KompasWrapper();
            }
            else
            {
                return new AutoCadWrapper();
            }
        }
    }
}

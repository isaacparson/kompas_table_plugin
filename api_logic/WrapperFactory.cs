using KompasWrapperLib;
using InventorWrapperLib;
using WrapperLib;

namespace ApiLogic
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

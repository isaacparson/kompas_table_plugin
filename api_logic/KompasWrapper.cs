using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using KompasAPI7;
using Kompas6API5;
using System.Runtime.InteropServices;
using Kompas6Constants;

namespace api_logic
{
    public class KompasWrapper : IWrapper
    {
        public KompasWrapper() 
        {

        }

        public void CreateRectangle()
        {
            
        }

        public void CreateSketch()
        {
            
        }

        public void Extrude()
        {
            
        }

        public void OpenCad()
        {
            IApplication application = (IApplication)Marshal.GetActiveObject("KOMPAS.Application.7");
            application.Documents.Add(DocumentTypeEnum.ksDocumentPart);
            var part = application.ActiveDocument;
        }
    }
}

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
using Kompas6Constants3D;
using logic;

namespace api_logic
{
    public class KompasWrapper : IWrapper
    {
        private static IKompasAPIObject _kompas7;

        public KompasWrapper() 
        {

        }

        private void NewRectangle()
        {
            
        }

        private void Extrude()
        {
            
        }

        public void CreatePart(Parameters parameters)
        {
            _kompas7.Application.Documents.Add(DocumentTypeEnum.ksDocumentPart);
            IKompasDocument3D document3D = (IKompasDocument3D)_kompas7.Application.ActiveDocument;
            IPart7 part = document3D.TopPart;

            IModelContainer modelContainer = (IModelContainer)part;

            ISketch sketch = modelContainer.Sketchs.Add();
            part = document3D.TopPart;
            sketch.Plane = part.DefaultObject[ksObj3dTypeEnum.o3d_planeXOY];
            sketch.Name = "Эскиз: столешница";
            sketch.Hidden = false;
            sketch.Angle = 90.0;
            //sketch.LeftHandedCS = true;
            sketch.Update();

            IKompasDocument documentSketch = sketch.BeginEdit();
            IKompasDocument2D document2D = (IKompasDocument2D)documentSketch;
            IViewsAndLayersManager viewsAndLayersManager = document2D.ViewsAndLayersManager;
            IView view = viewsAndLayersManager.Views.ActiveView;
            IDrawingContainer drawingContainer = (IDrawingContainer)view;

            IRectangle rectangle = drawingContainer.Rectangles.Add();
            rectangle.Style = (int)Kompas6Constants.ksCurveStyleEnum.ksCSNormal;

            Parameter topHeight;
            Parameter topDepth;
            parameters.Params.TryGetValue(ParamType.TopHeight, out topHeight);
            parameters.Params.TryGetValue(ParamType.TopHeight, out topDepth);

            rectangle.Width = topHeight.Value;
            rectangle.Height = topDepth.Value;
            rectangle.Update();
            sketch.EndEdit();
        }

        public void OpenCad()
        {
            Type t = Type.GetTypeFromProgID("KOMPAS.Application.7");
            _kompas7 = (IKompasAPIObject)Activator.CreateInstance(t);
            _kompas7.Application.Visible = true;
        }
    }
}

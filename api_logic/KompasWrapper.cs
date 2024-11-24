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
using System.Diagnostics;

namespace api_logic
{
    public class KompasWrapper : IWrapper
    {
        private static IKompasAPIObject _kompas7;
        private IPart7 _part;
        private IModelContainer _modelContainer;
        private ISketch _theLastCreatedSketch;

        public void NewRectangle(double x, double y, int width, int height, string name)
        {
            ISketch sketch = _modelContainer.Sketchs.Add();
            sketch.Plane = _part.DefaultObject[ksObj3dTypeEnum.o3d_planeXOY];
            sketch.Name = "Эскиз: " + name;
            sketch.Hidden = false;
            sketch.Update();

            IKompasDocument documentSketch = sketch.BeginEdit();
            IKompasDocument2D document2D = (IKompasDocument2D)documentSketch;
            IViewsAndLayersManager viewsAndLayersManager = document2D.ViewsAndLayersManager;
            IView view = viewsAndLayersManager.Views.ActiveView;
            IDrawingContainer drawingContainer = (IDrawingContainer)view;

            IRectangle rectangle = drawingContainer.Rectangles.Add();
            rectangle.Style = (int)Kompas6Constants.ksCurveStyleEnum.ksCSNormal;
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = width;
            rectangle.Height = height;

            rectangle.Update();
            sketch.EndEdit();

            _theLastCreatedSketch = sketch;
        }

        public void Extrude(int height, string name, bool positiveDirection)
        {
            IExtrusions extrusions = _modelContainer.Extrusions;
            IExtrusion extrusion = extrusions.Add(Kompas6Constants3D.ksObj3dTypeEnum.o3d_bossExtrusion);
            extrusion.Direction = Kompas6Constants3D.ksDirectionTypeEnum.dtNormal;
            extrusion.Name = "Элемент выдавливания: " + name;
            extrusion.Hidden = false;
            extrusion.ExtrusionType[true] = Kompas6Constants3D.ksEndTypeEnum.etBlind;
            extrusion.DraftOutward[true] = false;
            extrusion.DraftValue[true] = 0.0;
            if (positiveDirection)
            {
                extrusion.Depth[true] = height;
            }
            else
            {
                extrusion.Depth[true] = -height;
            }
            

            IExtrusion1 extrusion1 = (IExtrusion1)extrusion;
            extrusion1.Profile = _theLastCreatedSketch;
            extrusion1.DirectionObject = _theLastCreatedSketch;
            extrusion.Update();
        }

        public void OpenCad()
        {
            Type t = Type.GetTypeFromProgID("KOMPAS.Application.7");
            _kompas7 = (IKompasAPIObject)Activator.CreateInstance(t);
            _kompas7.Application.Visible = true;
        }

        public void CreatePart()
        {
            _kompas7.Application.Documents.Add(DocumentTypeEnum.ksDocumentPart);
            IKompasDocument3D document3D = (IKompasDocument3D)_kompas7.Application.ActiveDocument;
            _part = document3D.TopPart;
            _modelContainer = (IModelContainer)_part;
        }
    }
}

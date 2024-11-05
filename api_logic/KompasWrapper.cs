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

            // Столешница
            Parameter topWidth;
            Parameter topDepth;
            Parameter topHeight;
            parameters.Params.TryGetValue(ParamType.TopWidth, out topWidth);
            parameters.Params.TryGetValue(ParamType.TopDepth, out topDepth);
            parameters.Params.TryGetValue(ParamType.TopHeight, out topHeight);
            {
                ISketch sketch = modelContainer.Sketchs.Add();
                part = document3D.TopPart;
                sketch.Plane = part.DefaultObject[ksObj3dTypeEnum.o3d_planeXOY];
                sketch.Name = "Эскиз: столешница";
                sketch.Hidden = false;
                sketch.Update();

                IKompasDocument documentSketch = sketch.BeginEdit();
                IKompasDocument2D document2D = (IKompasDocument2D)documentSketch;
                IViewsAndLayersManager viewsAndLayersManager = document2D.ViewsAndLayersManager;
                IView view = viewsAndLayersManager.Views.ActiveView;
                IDrawingContainer drawingContainer = (IDrawingContainer)view;

                IRectangle rectangle = drawingContainer.Rectangles.Add();
                rectangle.Style = (int)Kompas6Constants.ksCurveStyleEnum.ksCSNormal;

                rectangle.Width = topWidth.Value;
                rectangle.Height = topDepth.Value;
                rectangle.Update();
                sketch.EndEdit();

                //объект модели "Операция выдавливания"
                IExtrusions extrusions = modelContainer.Extrusions;
                IExtrusion extrusion = extrusions.Add(Kompas6Constants3D.ksObj3dTypeEnum.o3d_bossExtrusion);
                extrusion.Direction = Kompas6Constants3D.ksDirectionTypeEnum.dtNormal;
                extrusion.Name = "Элемент выдавливания: столешница";
                extrusion.Hidden = false;
                extrusion.ExtrusionType[true] = Kompas6Constants3D.ksEndTypeEnum.etBlind;
                extrusion.DraftOutward[true] = false;
                extrusion.DraftValue[true] = 0.0;
                extrusion.Depth[true] = topHeight.Value;

                IExtrusion1 extrusion1 = (IExtrusion1)extrusion;
                extrusion1.Profile = sketch;// modelContainer.Objects[Kompas6Constants3D.ksObj3dTypeEnum.o3d_sketch][0];
                extrusion1.DirectionObject = sketch;// modelContainer.Objects[Kompas6Constants3D.ksObj3dTypeEnum.o3d_sketch][0];
                extrusion.Update();
            }
            // \Столешница

            // Ножки
            Parameter legWidth;
            Parameter tableHeight;
            parameters.Params.TryGetValue(ParamType.LegWidth, out legWidth);
            parameters.Params.TryGetValue(ParamType.TableHeight, out tableHeight);
            {
                ISketch sketch = modelContainer.Sketchs.Add();
                part = document3D.TopPart;
                sketch.Plane = part.DefaultObject[ksObj3dTypeEnum.o3d_planeXOY];
                sketch.Name = "Эскиз: ножки";
                sketch.Hidden = false;
                sketch.Update();

                IKompasDocument documentSketch = sketch.BeginEdit();
                IKompasDocument2D document2D = (IKompasDocument2D)documentSketch;
                IViewsAndLayersManager viewsAndLayersManager = document2D.ViewsAndLayersManager;
                IView view = viewsAndLayersManager.Views.ActiveView;
                IDrawingContainer drawingContainer = (IDrawingContainer)view;

                IRectangle rectangle1 = drawingContainer.Rectangles.Add();
                rectangle1.Style = (int)Kompas6Constants.ksCurveStyleEnum.ksCSNormal;
                rectangle1.Width = legWidth.Value;
                rectangle1.Height = rectangle1.Width;
                rectangle1.Update();

                IRectangle rectangle2 = drawingContainer.Rectangles.Add();
                rectangle2.Style = (int)Kompas6Constants.ksCurveStyleEnum.ksCSNormal;
                rectangle2.X = topWidth.Value - legWidth.Value;
                rectangle2.Width = legWidth.Value;
                rectangle2.Height = rectangle2.Width;
                rectangle2.Update();

                IRectangle rectangle3 = drawingContainer.Rectangles.Add();
                rectangle3.Style = (int)Kompas6Constants.ksCurveStyleEnum.ksCSNormal;
                rectangle3.Y = topDepth.Value - legWidth.Value;
                rectangle3.Width = legWidth.Value;
                rectangle3.Height = rectangle3.Width;
                rectangle3.Update();

                IRectangle rectangle4 = drawingContainer.Rectangles.Add();
                rectangle4.Style = (int)Kompas6Constants.ksCurveStyleEnum.ksCSNormal;
                rectangle4.X = topWidth.Value - legWidth.Value;
                rectangle4.Y = topDepth.Value - legWidth.Value;
                rectangle4.Width = legWidth.Value;
                rectangle4.Height = rectangle4.Width;
                rectangle4.Update();

                sketch.EndEdit();

                //объект модели "Операция выдавливания"
                IExtrusions extrusions = modelContainer.Extrusions;
                IExtrusion extrusion = extrusions.Add(Kompas6Constants3D.ksObj3dTypeEnum.o3d_bossExtrusion);
                extrusion.Direction = Kompas6Constants3D.ksDirectionTypeEnum.dtNormal;
                extrusion.Name = "Элемент выдавливания: ножки";
                extrusion.Hidden = false;
                extrusion.ExtrusionType[true] = Kompas6Constants3D.ksEndTypeEnum.etBlind;
                extrusion.DraftOutward[true] = false;
                extrusion.DraftValue[true] = 0.0;
                extrusion.Depth[true] = -(tableHeight.Value - topHeight.Value);
                extrusion.Direction = ksDirectionTypeEnum.dtNormal;

                IExtrusion1 extrusion1 = (IExtrusion1)extrusion;
                extrusion1.Profile = sketch;

                extrusion.Update();

            }
        }

        public void OpenCad()
        {
            Type t = Type.GetTypeFromProgID("KOMPAS.Application.7");
            _kompas7 = (IKompasAPIObject)Activator.CreateInstance(t);
            _kompas7.Application.Visible = true;
        }
    }
}

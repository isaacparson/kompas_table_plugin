using System;
using KompasAPI7;
using Kompas6Constants;
using Kompas6Constants3D;
using WrapperLib;

namespace KompasWrapperLib
{
    /// <summary>
    /// Класс, реализующий базовое взаимодействие с API Компас 3D
    /// </summary>
    public class KompasWrapper : IWrapper
    {
        /// <summary>
        /// Инстанс Компаса.
        /// </summary>
        private static IKompasAPIObject _kompas7;

        /// <summary>
        /// Документ- деталь.
        /// </summary>
        private IPart7 _part;

        /// <summary>
        /// Контейнер моделей.
        /// </summary>
        private IModelContainer _modelContainer;

        /// <summary>
        /// Последний созданный эскиз.
        /// </summary>
        private ISketch _theLastCreatedSketch;

        /// <summary>
        /// Создать эскиз с прямоугольником
        /// </summary>
        /// <param name="x">Координата X начала</param>
        /// <param name="y">Координата Y начала</param>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <param name="name">Имя эскиза</param>
        public void NewRectangle(double x, double y, int width, int height, string name)
        {
            if (width == 0 || height == 0)
            {
                throw new WrapperNewRectangleException(
                    "Rectangle build failed: one or both dimensions are zero");
            }
            if (_modelContainer == null)
            {
                throw new WrapperNewRectangleException(
                    "Rectangle build failed: there is no part for sketch");
            }
            ISketch sketch = _modelContainer.Sketchs.Add();
            sketch.Plane = _part.DefaultObject[ksObj3dTypeEnum.o3d_planeXOY];
            sketch.Name = "Эскиз: " + name;
            sketch.Hidden = false;
            if (!sketch.Update())
            {
                throw new WrapperNewRectangleException("Sketch update failed");
            }

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

            if (!rectangle.Update())
            {
                throw new WrapperNewRectangleException("Rectangle update failed");
            }
            if (!sketch.EndEdit())
            {
                throw new WrapperNewRectangleException("Sketch end edit failed");
            }

            _theLastCreatedSketch = sketch;
        }

        /// <summary>
        /// Выдавить последний созданный эскиз по оси OZ
        /// </summary>
        /// <param name="height">Глубина выдавливания</param>
        /// <param name="name">Имя результирующего тела</param>
        /// <param name="positiveDirection">Направление выдавливания - в положительную сторону оси</param>
        public void Extrude(int height, string name, bool positiveDirection)
        {
            if (height <= 0)
            {
                throw new WrapperExtrudeException(
                    "Extrude failed: height is less than zero");
            }
            if (_theLastCreatedSketch == null)
            {
                throw new WrapperExtrudeException(
                    "Extrude failed: there is no sketch to extrude");
            }

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
            
            if (!extrusion.Update())
            {
                throw new WrapperExtrudeException("Extrusion update failed");
            }
        }

        /// <summary>
        /// Открыть САПР
        /// </summary>
        /// <exception cref="WrapperOpenCadException"></exception>
        public void OpenCad()
        {
            Type t = Type.GetTypeFromProgID("KOMPAS.Application.7");
            _kompas7 = (IKompasAPIObject)Activator.CreateInstance(t);
            if (_kompas7 == null)
            {
                throw new WrapperOpenCadException(
                    "Open Kompas failed: there is probably no kompas on device");
            }
            _kompas7.Application.Visible = true;
        }

        /// <summary>
        /// Создать деталь
        /// </summary>
        /// <exception cref="WrapperCreatePartException"></exception>
        public void CreatePart()
        {
            if (_kompas7 == null)
            {
                throw new WrapperCreatePartException(
                    "Create part failed: kompas is not running");
            }
            _kompas7.Application.Documents.Add(DocumentTypeEnum.ksDocumentPart);
            IKompasDocument3D document3D = (IKompasDocument3D)_kompas7.Application.ActiveDocument;
            _part = document3D.TopPart;
            _modelContainer = (IModelContainer)_part;
            System.Threading.Thread.Sleep(100);
        }

        /// <summary>
        /// Открыта ли САПР
        /// </summary>
        /// <returns></returns>
        public bool IsCadRunning()
        {
            return _kompas7 != null;
        }
    }
}

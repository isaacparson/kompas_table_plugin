using System;
using Inventor;
using WrapperLib;

namespace InventorWrapperLib
{
    public class InventorWrapper : IWrapper
    {
        Inventor.Application _inventorApp;
        PartDocument _partDocument;
        PlanarSketch _sketch;
        PartComponentDefinition _partCompDef;

        public void OpenCad()
        {

            Type invType = System.Type.GetTypeFromProgID("Inventor.Application");
            _inventorApp = System.Activator.CreateInstance(invType) as Inventor.Application;
            if (_inventorApp == null)
            {
                throw new WrapperOpenCadException("Open Inventor failed: there is probably no Inventor on device");
            }
            _inventorApp.Visible = true;
        }

        public void CreatePart()
        {
            if (_inventorApp == null)
            {
                throw new WrapperCreatePartException("Create part failed: Inventor is not running");
            }
            _partDocument = (PartDocument)_inventorApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject, _inventorApp.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject, SystemOfMeasureEnum.kEnglishSystemOfMeasure, DraftingStandardEnum.kDefault_DraftingStandard, "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}"));
        }

        public void NewRectangle(double x, double y, int width, int height, string name)
        {
            if (width == 0 || height == 0)
            {
                throw new WrapperNewRectangleException("Rectangle build failed: one or both dimensions are zero");
            }
            if (_partDocument == null)
            {
                throw new WrapperNewRectangleException("Rectangle build failed: there is no part for sketch");
            }
            try
            {
                _partCompDef = _partDocument.ComponentDefinition;
                _sketch = _partCompDef.Sketches.Add(_partCompDef.WorkPlanes[3]);
                _sketch.Name = name;
                TransientGeometry transGeo = _inventorApp.TransientGeometry;

                SketchEntitiesEnumerator rectangle = _sketch.SketchLines.AddAsTwoPointRectangle(transGeo.CreatePoint2d(x, y), transGeo.CreatePoint2d(x + width, y + height));
            }
            catch (Exception)
            {
                throw new WrapperNewRectangleException("Rectangle build failed");
            }
        }

        public void Extrude(int height, string name, bool positiveDirection)
        {
            if (height <= 0)
            {
                throw new WrapperExtrudeException("Extrude failed: height is less than zero");
            }
            if (_sketch == null)
            {
                throw new WrapperExtrudeException("Extrude failed: there is no sketch to extrude");
            }
            try
            {
                Profile profile = _sketch.Profiles.AddForSolid();
                ExtrudeDefinition extrudeDef = _partCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kJoinOperation);

                if (positiveDirection)
                {
                    extrudeDef.SetDistanceExtent(height, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
                }
                else
                {
                    extrudeDef.SetDistanceExtent(height, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
                }

                ExtrudeFeature extrude = _partCompDef.Features.ExtrudeFeatures.Add(extrudeDef);
                extrude.Name = name + " ";
            }
            catch (Exception)
            {
                throw new WrapperExtrudeException("Extrude failed");
            }
        }

        public bool IsCadRunning()
        {
            return _inventorApp != null;
        }
    }
}

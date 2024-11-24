using logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using System.Runtime.InteropServices;

namespace api_logic
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
            _inventorApp.Visible = true;
        }

        public void CreatePart()
        {
            _partDocument = (PartDocument)_inventorApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject, _inventorApp.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject, SystemOfMeasureEnum.kEnglishSystemOfMeasure, DraftingStandardEnum.kDefault_DraftingStandard, "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}"));
        }

        public void NewRectangle(double x, double y, int width, int height, string name)
        {
            _partCompDef = _partDocument.ComponentDefinition;
            _sketch = _partCompDef.Sketches.Add(_partCompDef.WorkPlanes[3]);
            _sketch.Name = name;
            TransientGeometry transGeo = _inventorApp.TransientGeometry;

            SketchEntitiesEnumerator rectangle = _sketch.SketchLines.AddAsTwoPointRectangle(transGeo.CreatePoint2d(x, y), transGeo.CreatePoint2d(x + width, y + height));
        }

        public void Extrude(int height, string name, bool positiveDirection)
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
    }
}

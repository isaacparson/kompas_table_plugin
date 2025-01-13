﻿using System;
using Inventor;
using WrapperLib;

namespace InventorWrapperLib
{
    /// <summary>
    /// Класс, еализующий базовое взаимодействие с API Autodesk Inventor
    /// </summary>
    public class InventorWrapper : IWrapper
    {
        /// <summary>
        /// Инстанс инвентора.
        /// </summary>
        private Inventor.Application _inventorApp;

        /// <summary>
        /// Документ- деталь.
        /// </summary>
        private PartDocument _partDocument;

        /// <summary>
        /// Документ- эскиз.
        /// </summary>
        private PlanarSketch _sketch;

        /// <summary>
        /// Определение детали.
        /// </summary>
        private PartComponentDefinition _partCompDef;

        ///<inheritdoc cref='IWrapper.OpenCad()'>
        public void OpenCad()
        {

            var invType = System.Type.GetTypeFromProgID("Inventor.Application");
            _inventorApp = System.Activator.CreateInstance(invType) as Inventor.Application;
            if (_inventorApp == null)
            {
                throw new WrapperOpenCadException(
                    "Open Inventor failed: there is probably no Inventor on device");
            }
            _inventorApp.Visible = true;
        }

        ///<inheritdoc cref='IWrapper.CreatePart()'>
        public void CreatePart()
        {
            if (_inventorApp == null)
            {
                throw new WrapperCreatePartException(
                    "Create part failed: Inventor is not running");
            }
            _partDocument = (PartDocument)_inventorApp.Documents.Add(
                DocumentTypeEnum.kPartDocumentObject, 
                _inventorApp.FileManager.GetTemplateFile(
                    DocumentTypeEnum.kPartDocumentObject, 
                    SystemOfMeasureEnum.kEnglishSystemOfMeasure, 
                    DraftingStandardEnum.kDefault_DraftingStandard, 
                    "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}"));
        }

        ///<inheritdoc cref='IWrapper.NewRectangle(double, double, int, int, string)'>
        public void NewRectangle(double x, double y, int width, int height, string name)
        {
            if (width == 0 || height == 0)
            {
                throw new WrapperNewRectangleException(
                    "Rectangle build failed: one or both dimensions are zero");
            }
            if (_partDocument == null)
            {
                throw new WrapperNewRectangleException(
                    "Rectangle build failed: there is no part for sketch");
            }
            try
            {
                _partCompDef = _partDocument.ComponentDefinition;
                _sketch = _partCompDef.Sketches.Add(_partCompDef.WorkPlanes[3]);
                _sketch.Name = name;
                var transGeo = _inventorApp.TransientGeometry;

                var rectangle = _sketch.SketchLines.AddAsTwoPointRectangle(
                    transGeo.CreatePoint2d(x, y), 
                    transGeo.CreatePoint2d(x + width, y + height));
            }
            catch (Exception)
            {
                throw new WrapperNewRectangleException("Rectangle build failed");
            }
        }

        ///<inheritdoc cref='IWrapper.Extrude(int, string, bool)'>
        public void Extrude(int height, string name, bool positiveDirection)
        {
            if (height <= 0)
            {
                throw new WrapperExtrudeException(
                    "Extrude failed: height is less than zero");
            }
            if (_sketch == null)
            {
                throw new WrapperExtrudeException(
                    "Extrude failed: there is no sketch to extrude");
            }
            try
            {
                var profile = _sketch.Profiles.AddForSolid();
                var extrudeDef = _partCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(
                    profile, 
                    PartFeatureOperationEnum.kJoinOperation);

                if (positiveDirection)
                {
                    extrudeDef.SetDistanceExtent(
                        height, 
                        PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
                }
                else
                {
                    extrudeDef.SetDistanceExtent(
                        height, 
                        PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
                }

                var extrude = _partCompDef.Features.ExtrudeFeatures.Add(extrudeDef);
                extrude.Name = name + " ";
            }
            catch (Exception)
            {
                throw new WrapperExtrudeException("Extrude failed");
            }
        }

        ///<inheritdoc cref='IWrapper.IsCadRunning()'>
        public bool IsCadRunning()
        {
            return _inventorApp != null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Image : ApiDataObject
    {
        public Image()
        {
        }

        public void CalculateDectProtonStoppingPowers(Image rhoImage, Image zImage, int planeIndex, double[,] preallocatedBuffer) { }
        public StructureSet CreateNewStructureSet() => default;
        public VVector DicomToUser(VVector dicom, PlanSetup planSetup) => default;
        public ImageProfile GetImageProfile(VVector start, VVector stop, double[] preallocatedBuffer) => default;
        public bool GetProtonStoppingPowerCurve(SortedList<double, double> protonStoppingPowerCurve) => default;
        public void GetVoxels(int planeIndex, int[,] preallocatedBuffer) { }
        public VVector UserToDicom(VVector user, PlanSetup planSetup) => default;
        public double VoxelToDisplayValue(int voxelValue) => default;
        public IEnumerable<ImageApprovalHistoryEntry> ApprovalHistory { get; set; }
        public DateTime? CalibrationProtocolDateTime { get; set; }
        public string CalibrationProtocolDescription { get; set; }
        public string CalibrationProtocolId { get; set; }
        public string CalibrationProtocolImageMatchWarning { get; set; }
        public DateTime? CalibrationProtocolLastModifiedDateTime { get; set; }
        public string ContrastBolusAgentIngredientName { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public string DisplayUnit { get; set; }
        public string FOR { get; set; }
        public bool HasUserOrigin { get; set; }
        public string ImageType { get; set; }
        public string ImagingDeviceId { get; set; }
        public PatientOrientation ImagingOrientation { get; set; }
        public string ImagingOrientationAsString { get; set; }
        public bool IsProcessed { get; set; }
        public int Level { get; set; }
        public SeriesModality Modality { get; set; }
        public VVector Origin { get; set; }
        public Series Series { get; set; }
        public string UID { get; set; }
        public VVector UserOrigin { get; set; }
        public string UserOriginComments { get; set; }
        public int Window { get; set; }
        public VVector XDirection { get; set; }
        public double XRes { get; set; }
        public int XSize { get; set; }
        public VVector YDirection { get; set; }
        public double YRes { get; set; }
        public int YSize { get; set; }
        public VVector ZDirection { get; set; }
        public double ZRes { get; set; }
        public int ZSize { get; set; }
    }
}

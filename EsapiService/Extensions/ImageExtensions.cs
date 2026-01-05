using Esapi.Interfaces;
using Esapi.Services;
using System;
using System.Collections.Generic;

namespace Esapi.Interfaces
{
    public partial interface IImage : IApiDataObject
    {
        /// <summary>
        /// Gets the Z-coordinate (in mm) of the center of the specified slice index.
        /// Replaces: Origin.z + (Index * ZRes * ZDirection.z)
        /// </summary>
        double GetSliceZ(int sliceIndex);

        /// <summary>
        /// Returns all slice Z-coordinates as an enumerable collection.
        /// Useful if you need to iterate or use LINQ (e.g., image.GetSlices().Where(...))
        /// </summary>
        IEnumerable<double> GetSlices();

        /// <summary>
        /// Converts a Z-coordinate (in mm) to the corresponding fractional Slice Index.
        /// Inverse of GetSliceZ.
        /// </summary>
        int GetSliceIndexFromZ(double zMm);

        /// <summary>
        /// Calculates the inclusive range of integer slice indices corresponding to a Z-range in mm.
        /// Handles sorting (HFS vs FFS) and boundary inclusion logic automatically.
        /// </summary>
        (int StartSlice, int EndSlice) GetSliceRange(double zStartMm, double zEndMm);
    }
}

namespace Esapi.Wrappers
{
    public partial class AsyncImage : AsyncApiDataObject, IImage, IEsapiWrapper<VMS.TPS.Common.Model.API.Image>
    {
        /// <summary>
        /// Gets the Z-coordinate (in mm) of the center of the specified slice index.
        /// Replaces: Origin.z + (Index * ZRes * ZDirection.z)
        /// </summary>
        public double GetSliceZ(int sliceIndex) =>
            Origin.z + (sliceIndex * ZRes * ZDirection.z);

        /// <summary>
        /// Returns all slice Z-coordinates as an enumerable collection.
        /// Useful if you need to iterate or use LINQ (e.g., image.GetSlices().Where(...))
        /// </summary>
        public IEnumerable<double> GetSlices()
        {
            for (int i = 0; i < ZSize; i++)
            {
                yield return GetSliceZ(i);
            }
        }

        /// <summary>
        /// Converts a Z-coordinate (in mm) to the corresponding fractional Slice Index.
        /// Inverse of GetSliceZ.
        /// </summary>
        public int GetSliceIndexFromZ(double zMm) =>
            // Formula: Index = int ( (Z - Origin) / (Res * Dir) )
            (int)((zMm - Origin.z) / (ZRes * ZDirection.z));

        /// <summary>
        /// Calculates the inclusive range of integer slice indices corresponding to a Z-range in mm.
        /// Handles sorting (HFS vs FFS) and boundary inclusion logic automatically.
        /// </summary>
        public (int StartSlice, int EndSlice) GetSliceRange(double zStartMm, double zEndMm)
        {
            const double epsilon = 1E-3;

            // Convert Z-coords to fractional indices
            double idx1 = GetSliceIndexFromZ(zStartMm);
            double idx2 = GetSliceIndexFromZ(zEndMm);

            // Sort to find the Image-Space range (Min Index to Max Index)
            double minIdxFloat = Math.Min(idx1, idx2);
            double maxIdxFloat = Math.Max(idx1, idx2);

            // Apply Inclusion Logic (Keep slices that are effectively inside the bounds)
            // Start: Ceil snaps "inward" (e.g. 10.1 -> 11)
            int startSlice = (int)Math.Ceiling(minIdxFloat - epsilon);

            // End: Floor snaps "inward" (e.g. 20.9 -> 20)
            int endSlice = (int)Math.Floor(maxIdxFloat + epsilon);

            // Clamp to actual image dimensions
            startSlice = Math.Max(0, Math.Min(ZSize - 1, startSlice));
            endSlice = Math.Max(0, Math.Min(ZSize - 1, endSlice));

            return (startSlice, endSlice);
        }
    }
}

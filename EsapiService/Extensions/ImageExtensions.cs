using Esapi.Interfaces;
using System;
using System.Collections.Generic;

namespace Esapi.Extensions
{
    public static class IImageExtensions
    {
        /// <summary>
        /// Gets the Z-coordinate (in mm) of the center of the specified slice index.
        /// Replaces: Origin.z + (Index * ZRes * ZDirection.z)
        /// </summary>
        public static double GetSliceZ(this IImage image, int sliceIndex) =>
            image.Origin.z + (sliceIndex * image.ZRes * image.ZDirection.z);

        /// <summary>
        /// Returns all slice Z-coordinates as an enumerable collection.
        /// Useful if you need to iterate or use LINQ (e.g., image.GetSlices().Where(...))
        /// </summary>
        public static IEnumerable<double> GetSlices(this IImage image)
        {
            for (int i = 0; i < image.ZSize; i++)
            {
                yield return image.GetSliceZ(i);
            }
        }

        /// <summary>
        /// Converts a Z-coordinate (in mm) to the corresponding fractional Slice Index.
        /// Inverse of GetSliceZ.
        /// </summary>
        public static int GetSliceIndexFromZ(this IImage image, double zMm) =>
            // Formula: Index = int ( (Z - Origin) / (Res * Dir) )
            (int)((zMm - image.Origin.z) / (image.ZRes * image.ZDirection.z));

        /// <summary>
        /// Calculates the inclusive range of integer slice indices corresponding to a Z-range in mm.
        /// Handles sorting (HFS vs FFS) and boundary inclusion logic automatically.
        /// </summary>
        public static (int StartSlice, int EndSlice) GetSliceRange(this IImage image, double zStartMm, double zEndMm)
        {
            const double epsilon = 1E-3;

            // Convert Z-coords to fractional indices
            double idx1 = image.GetSliceIndexFromZ(zStartMm);
            double idx2 = image.GetSliceIndexFromZ(zEndMm);

            // Sort to find the Image-Space range (Min Index to Max Index)
            double minIdxFloat = Math.Min(idx1, idx2);
            double maxIdxFloat = Math.Max(idx1, idx2);

            // Apply Inclusion Logic (Keep slices that are effectively inside the bounds)
            // Start: Ceil snaps "inward" (e.g. 10.1 -> 11)
            int startSlice = (int)Math.Ceiling(minIdxFloat - epsilon);

            // End: Floor snaps "inward" (e.g. 20.9 -> 20)
            int endSlice = (int)Math.Floor(maxIdxFloat + epsilon);

            // Clamp to actual image dimensions
            startSlice = Math.Max(0, Math.Min(image.ZSize - 1, startSlice));
            endSlice = Math.Max(0, Math.Min(image.ZSize - 1, endSlice));

            return (startSlice, endSlice);
        }
    }
}

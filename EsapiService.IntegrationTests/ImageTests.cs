using Esapi.Interfaces;
using EsapiTestAdapter;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EsapiService.IntegrationTests
{

    public class ImageTests : EsapiTestBase
    {
        private const string PatientId = "OPTIMATE_TEST_PATIENT";
        private const string PlanId = "Plan01";

        private IPatient _patient;
        private IPlanSetup _plan;
        private IImage _image;

        [EsapiSetup]
        public async Task Setup()
        {
            _patient = await Esapi.OpenPatientByIdAsync(PatientId);
            _plan = await Esapi.OpenPlanByIdAsync(PlanId);
            _image = await _plan.GetImageAsync();

            if (_image is null)
                Assert.Inconclusive("Setup failed: StructureSet has no 3D Image.");
        }

        [EsapiTest]
        public void GetSliceZ_ShouldMatchOrigin_AtIndexZero()
        {
            // Act
            double zZero = _image.GetSliceZ(0);

            // Assert
            // Slice 0 should effectively be the Origin Z (plus/minus half pixel offset depending on Varian definition, 
            // but usually Origin IS the center of the first slice in DICOM).
            // We allow a tiny delta for floating point precision.
            Assert.AreEqual(_image.Origin.z, zZero, 0.001, "Slice 0 Z should equal Image Origin Z.");
        }

        [EsapiTest]
        public void GetSliceZ_ShouldCalculateCorrectly_ForMiddleSlice()
        {
            // Arrange
            int sliceIndex = 10;
            // Manual calc: Origin + (Index * Res * Dir)
            double expectedZ = _image.Origin.z + (sliceIndex * _image.ZRes * _image.ZDirection.z);

            // Act
            double actualZ = _image.GetSliceZ(sliceIndex);

            // Assert
            Assert.AreEqual(expectedZ, actualZ, 0.001);
        }

        [EsapiTest]
        public void GetSlices_ShouldReturnAllSlices()
        {
            // Act
            var slices = _image.GetSlices().ToList();

            // Assert
            Assert.AreEqual(_image.ZSize, slices.Count, "Should return exactly ZSize number of slices.");

            // Verify ordering (slices should change by ZRes)
            double diff = Math.Abs(slices[1] - slices[0]);
            Assert.AreEqual(_image.ZRes, diff, 0.001, "Distance between slices should equal ZRes.");
        }

        [EsapiTest]
        public void GetSliceIndexFromZ_ShouldBeInverseOf_GetSliceZ()
        {
            // Test "Round Trip" conversion
            int originalIndex = 5;

            // 1. Convert Index -> MM
            double zMm = _image.GetSliceZ(originalIndex);

            // 2. Convert MM -> Index
            int calculatedIndex = _image.GetSliceIndexFromZ(zMm);

            Assert.AreEqual(originalIndex, calculatedIndex, "Converting Z back to Index should return the original index.");
        }

        [EsapiTest]
        public void GetSliceRange_ShouldInclude_PartialSlices()
        {
            // Arrange
            // Let's define a range that starts halfway through slice 2 and ends halfway through slice 4.
            // Indices: 2, 3, 4 should be included.
            double zOfSlice2 = _image.GetSliceZ(2);
            double zOfSlice4 = _image.GetSliceZ(4);

            // Add a tiny offset to go "inside" the range if needed, or stick to centers.
            // If we ask for exactly Z(2) to Z(4), it should return [2, 4].

            // Act
            var range = _image.GetSliceRange(zOfSlice2, zOfSlice4);

            // Assert
            // Note: Since ZDirection can be negative, Min/Max logic in your code handles the sorting.
            // So passing (Z_Slice2, Z_Slice4) should work even if Z_Slice2 > Z_Slice4.
            Assert.AreEqual(2, range.StartSlice);
            Assert.AreEqual(4, range.EndSlice);
        }

        [EsapiTest]
        public void GetSliceRange_ShouldClamp_ToImageBoundaries()
        {
            // Arrange
            // Create a range that goes WAY outside the image
            double deepInside = _image.Origin.z - (1000 * _image.ZDirection.z); // Way "before" slice 0
            double wayOutside = _image.Origin.z + (1000 * _image.ZDirection.z); // Way "after" last slice

            // Act
            var range = _image.GetSliceRange(deepInside, wayOutside);

            // Assert
            Assert.AreEqual(0, range.StartSlice, "Start slice should clamp to 0.");
            Assert.AreEqual(_image.ZSize - 1, range.EndSlice, "End slice should clamp to ZSize - 1.");
        }
    }
}
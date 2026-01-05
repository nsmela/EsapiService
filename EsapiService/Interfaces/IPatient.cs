using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;

namespace Esapi.Interfaces
{
    public partial interface IPatient : IApiDataObject
    {
        // --- Simple Properties --- //
        DateTime? CreationDateTime { get; } // simple property
        DateTime? DateOfBirth { get; } // simple property
        string DefaultDepartment { get; } // simple property
        string FirstName { get; set; } // simple property
        bool HasModifiedData { get; } // simple property
        string Id2 { get; } // simple property
        string LastName { get; set; } // simple property
        string MiddleName { get; set; } // simple property
        string PrimaryOncologistId { get; } // simple property
        string PrimaryOncologistName { get; } // simple property
        string Sex { get; } // simple property
        string SSN { get; } // simple property

        // --- Accessors --- //
        Task<IHospital> GetHospitalAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<ICourse>> GetCoursesAsync(); // collection property context
        Task<IReadOnlyList<IReferencePoint>> GetReferencePointsAsync(); // collection property context
        Task<IReadOnlyList<IRegistration>> GetRegistrationsAsync(); // collection property context
        Task<IReadOnlyList<IStructureSet>> GetStructureSetsAsync(); // collection property context
        Task<IReadOnlyList<IStudy>> GetStudiesAsync(); // collection property context

        // --- Methods --- //
        Task<ICourse> AddCourseAsync(); // complex method
        Task<IStructureSet> AddEmptyPhantomAsync(string imageId, PatientOrientation orientation, int xSizePixel, int ySizePixel, double widthMM, double heightMM, int nrOfPlanes, double planeSepMM); // complex method
        Task<IReferencePoint> AddReferencePointAsync(bool target, string id); // complex method
        Task BeginModificationsAsync(); // void method
        Task<bool> CanAddCourseAsync(); // simple method
        Task<(bool result, string errorMessage)> CanAddEmptyPhantomAsync(); // out/ref parameter method
        Task<(bool result, string errorMessage)> CanCopyImageFromOtherPatientAsync(IStudy targetStudy, string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId); // out/ref parameter method
        Task<bool> CanModifyDataAsync(); // simple method
        Task<bool> CanRemoveCourseAsync(ICourse course); // simple method
        Task<(bool result, string errorMessage)> CanRemoveEmptyPhantomAsync(IStructureSet structureset); // out/ref parameter method
        Task<IStructureSet> CopyImageFromOtherPatientAsync(string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId); // complex method
        Task<IStructureSet> CopyImageFromOtherPatientAsync(IStudy targetStudy, string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId); // complex method
        Task RemoveCourseAsync(ICourse course); // void method
        Task RemoveEmptyPhantomAsync(IStructureSet structureset); // void method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Patient object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Patient> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Patient object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Patient, T> func);

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        new bool IsValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        new bool IsNotValid();
    }
}

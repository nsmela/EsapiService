using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;
using Esapi.Interfaces;

namespace Esapi.Interfaces
{
    public interface IPatient : IApiDataObject
    {
        // --- Simple Properties --- //
        string FirstName { get; }
        Task SetFirstNameAsync(string value);
        bool HasModifiedData { get; }
        string Id2 { get; }
        string LastName { get; }
        Task SetLastNameAsync(string value);
        string MiddleName { get; }
        Task SetMiddleNameAsync(string value);
        string PrimaryOncologistId { get; }
        string PrimaryOncologistName { get; }
        string Sex { get; }
        string SSN { get; }

        // --- Accessors --- //
        Task<IDepartment> GetDefaultDepartmentAsync();
        Task<IHospital> GetHospitalAsync();

        // --- Collections --- //
        Task<IReadOnlyList<ICourse>> GetCoursesAsync();
        IReadOnlyList<DateTime> CreationDateTime { get; }
        IReadOnlyList<DateTime> DateOfBirth { get; }
        Task<IReadOnlyList<IDepartment>> GetDepartmentsAsync();
        Task<IReadOnlyList<IReferencePoint>> GetReferencePointsAsync();
        Task<IReadOnlyList<IRegistration>> GetRegistrationsAsync();
        Task<IReadOnlyList<IStructureSet>> GetStructureSetsAsync();
        Task<IReadOnlyList<IStudy>> GetStudiesAsync();

        // --- Methods --- //
        Task<ICourse> AddCourseAsync();
        Task<IStructureSet> AddEmptyPhantomAsync(string imageId, PatientOrientation orientation, int xSizePixel, int ySizePixel, double widthMM, double heightMM, int nrOfPlanes, double planeSepMM);
        Task<IReferencePoint> AddReferencePointAsync(bool target, string id);
        Task BeginModificationsAsync();
        Task<bool> CanAddCourseAsync();
        Task<(bool Result, string errorMessage)> CanAddEmptyPhantomAsync();
        Task<(bool Result, string errorMessage)> CanCopyImageFromOtherPatientAsync(IStudy targetStudy, string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId);
        Task<bool> CanModifyDataAsync();
        Task<bool> CanRemoveCourseAsync(ICourse course);
        Task<(bool Result, string errorMessage)> CanRemoveEmptyPhantomAsync(IStructureSet structureset);
        Task<IStructureSet> CopyImageFromOtherPatientAsync(string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId);
        Task<IStructureSet> CopyImageFromOtherPatientAsync(IStudy targetStudy, string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId);
        Task RemoveCourseAsync(ICourse course);
        Task RemoveEmptyPhantomAsync(IStructureSet structureset);

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Patient object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Patient> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Patient object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Patient, T> func);
    }
}

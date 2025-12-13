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
    public interface IPatient : IApiDataObject
    {
        // --- Simple Properties --- //
        IEnumerable<Course> Courses { get; }
        DateTime? CreationDateTime { get; }
        DateTime? DateOfBirth { get; }
        IEnumerable<Department> Departments { get; }
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
        IEnumerable<ReferencePoint> ReferencePoints { get; }
        IEnumerable<Registration> Registrations { get; }
        string Sex { get; }
        string SSN { get; }
        IEnumerable<StructureSet> StructureSets { get; }
        IEnumerable<Study> Studies { get; }

        // --- Accessors --- //
        Task<IDepartment> GetDefaultDepartmentAsync(); // read complex property
        Task<IHospital> GetHospitalAsync(); // read complex property

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
    }
}

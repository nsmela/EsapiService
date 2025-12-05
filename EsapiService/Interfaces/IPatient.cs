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
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<ICourse> AddCourseAsync();
        Task<IStructureSet> AddEmptyPhantomAsync(string imageId, VMS.TPS.Common.Model.Types.PatientOrientation orientation, int xSizePixel, int ySizePixel, double widthMM, double heightMM, int nrOfPlanes, double planeSepMM);
        Task<IReferencePoint> AddReferencePointAsync(bool target, string id);
        Task BeginModificationsAsync();
        Task<bool> CanAddCourseAsync();
        Task<(bool Result, string errorMessage)> CanAddEmptyPhantomAsync();
        Task<(bool Result, string errorMessage)> CanCopyImageFromOtherPatientAsync(IStudy targetStudy, string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId);
        Task<bool> CanModifyDataAsync();
        Task<bool> CanRemoveCourseAsync(VMS.TPS.Common.Model.API.Course course);
        Task<(bool Result, string errorMessage)> CanRemoveEmptyPhantomAsync(IStructureSet structureset);
        Task<IStructureSet> CopyImageFromOtherPatientAsync(string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId);
        Task<IStructureSet> CopyImageFromOtherPatientAsync(VMS.TPS.Common.Model.API.Study targetStudy, string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId);
        Task RemoveCourseAsync(VMS.TPS.Common.Model.API.Course course);
        Task RemoveEmptyPhantomAsync(VMS.TPS.Common.Model.API.StructureSet structureset);
        System.Collections.Generic.IReadOnlyList<ICourse> Courses { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> DateOfBirth { get; }
        Task<IDepartment> GetDefaultDepartmentAsync();
        System.Collections.Generic.IReadOnlyList<IDepartment> Departments { get; }
        string FirstName { get; }
        Task SetFirstNameAsync(string value);
        bool HasModifiedData { get; }
        Task<IHospital> GetHospitalAsync();
        string Id2 { get; }
        string LastName { get; }
        Task SetLastNameAsync(string value);
        string MiddleName { get; }
        Task SetMiddleNameAsync(string value);
        string PrimaryOncologistId { get; }
        string PrimaryOncologistName { get; }
        System.Collections.Generic.IReadOnlyList<IReferencePoint> ReferencePoints { get; }
        System.Collections.Generic.IReadOnlyList<IRegistration> Registrations { get; }
        string Sex { get; }
        string SSN { get; }
        System.Collections.Generic.IReadOnlyList<IStructureSet> StructureSets { get; }
        System.Collections.Generic.IReadOnlyList<IStudy> Studies { get; }

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

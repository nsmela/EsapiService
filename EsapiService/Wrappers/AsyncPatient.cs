using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncPatient : AsyncApiDataObject, IPatient, IEsapiWrapper<VMS.TPS.Common.Model.API.Patient>
    {
        internal new readonly VMS.TPS.Common.Model.API.Patient _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncPatient(VMS.TPS.Common.Model.API.Patient inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            CreationDateTime = inner.CreationDateTime;
            DateOfBirth = inner.DateOfBirth;
            FirstName = inner.FirstName;
            HasModifiedData = inner.HasModifiedData;
            Id2 = inner.Id2;
            LastName = inner.LastName;
            MiddleName = inner.MiddleName;
            PrimaryOncologistId = inner.PrimaryOncologistId;
            PrimaryOncologistName = inner.PrimaryOncologistName;
            Sex = inner.Sex;
            SSN = inner.SSN;
        }


        public async Task<ICourse> AddCourseAsync()
        {
            return await _service.PostAsync(context => 
                _inner.AddCourse() is var result && result is null ? null : new AsyncCourse(result, _service));
        }

        public async Task<IStructureSet> AddEmptyPhantomAsync(string imageId, PatientOrientation orientation, int xSizePixel, int ySizePixel, double widthMM, double heightMM, int nrOfPlanes, double planeSepMM)
        {
            return await _service.PostAsync(context => 
                _inner.AddEmptyPhantom(imageId, orientation, xSizePixel, ySizePixel, widthMM, heightMM, nrOfPlanes, planeSepMM) is var result && result is null ? null : new AsyncStructureSet(result, _service));
        }

        public async Task<IReferencePoint> AddReferencePointAsync(bool target, string id)
        {
            return await _service.PostAsync(context => 
                _inner.AddReferencePoint(target, id) is var result && result is null ? null : new AsyncReferencePoint(result, _service));
        }

        // Simple Void Method
        public Task BeginModificationsAsync() 
        {
            _service.PostAsync(context => _inner.BeginModifications());
            Refresh();
            return Task.CompletedTask;
        }

        // Simple Method
        public Task<bool> CanAddCourseAsync() => 
            _service.PostAsync(context => _inner.CanAddCourse());

        public async Task<(bool result, string errorMessage)> CanAddEmptyPhantomAsync()
        {
            var postResult = await _service.PostAsync(context => {
                string errorMessage_temp = default(string);
                var result = _inner.CanAddEmptyPhantom(out errorMessage_temp);
                return (result, errorMessage_temp);
            });
            return (postResult.Item1,
                    postResult.Item2);
        }


        public async Task<(bool result, string errorMessage)> CanCopyImageFromOtherPatientAsync(IStudy targetStudy, string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId)
        {
            var postResult = await _service.PostAsync(context => {
                string errorMessage_temp = default(string);
                var result = _inner.CanCopyImageFromOtherPatient(((AsyncStudy)targetStudy)._inner, otherPatientId, otherPatientStudyId, otherPatient3DImageId, out errorMessage_temp);
                return (result, errorMessage_temp);
            });
            return (postResult.Item1,
                    postResult.Item2);
        }


        // Simple Method
        public Task<bool> CanModifyDataAsync() => 
            _service.PostAsync(context => _inner.CanModifyData());

        // Simple Method
        public Task<bool> CanRemoveCourseAsync(ICourse course) => 
            _service.PostAsync(context => _inner.CanRemoveCourse(((AsyncCourse)course)._inner));

        public async Task<(bool result, string errorMessage)> CanRemoveEmptyPhantomAsync(IStructureSet structureset)
        {
            var postResult = await _service.PostAsync(context => {
                string errorMessage_temp = default(string);
                var result = _inner.CanRemoveEmptyPhantom(((AsyncStructureSet)structureset)._inner, out errorMessage_temp);
                return (result, errorMessage_temp);
            });
            return (postResult.Item1,
                    postResult.Item2);
        }


        public async Task<IStructureSet> CopyImageFromOtherPatientAsync(string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId)
        {
            return await _service.PostAsync(context => 
                _inner.CopyImageFromOtherPatient(otherPatientId, otherPatientStudyId, otherPatient3DImageId) is var result && result is null ? null : new AsyncStructureSet(result, _service));
        }

        public async Task<IStructureSet> CopyImageFromOtherPatientAsync(IStudy targetStudy, string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId)
        {
            return await _service.PostAsync(context => 
                _inner.CopyImageFromOtherPatient(((AsyncStudy)targetStudy)._inner, otherPatientId, otherPatientStudyId, otherPatient3DImageId) is var result && result is null ? null : new AsyncStructureSet(result, _service));
        }

        // Simple Void Method
        public Task RemoveCourseAsync(ICourse course) 
        {
            _service.PostAsync(context => _inner.RemoveCourse(((AsyncCourse)course)._inner));
            Refresh();
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task RemoveEmptyPhantomAsync(IStructureSet structureset) 
        {
            _service.PostAsync(context => _inner.RemoveEmptyPhantom(((AsyncStructureSet)structureset)._inner));
            Refresh();
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<ICourse>> GetCoursesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Courses?.Select(x => new AsyncCourse(x, _service)).ToList());
        }


        public DateTime? CreationDateTime { get; private set; }


        public DateTime? DateOfBirth { get; private set; }


        public async Task<IDepartment> GetDefaultDepartmentAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.DefaultDepartment is null ? null : new AsyncDepartment(_inner.DefaultDepartment, _service);
                return innerResult;
            });
        }

        public async Task<IReadOnlyList<IDepartment>> GetDepartmentsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Departments?.Select(x => new AsyncDepartment(x, _service)).ToList());
        }


        public string FirstName { get; private set; }
        public async Task SetFirstNameAsync(string value)
        {
            FirstName = await _service.PostAsync(context => 
            {
                _inner.FirstName = value;
                return _inner.FirstName;
            });
        }


        public bool HasModifiedData { get; private set; }


        public async Task<IHospital> GetHospitalAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Hospital is null ? null : new AsyncHospital(_inner.Hospital, _service);
                return innerResult;
            });
        }

        public string Id2 { get; private set; }


        public string LastName { get; private set; }
        public async Task SetLastNameAsync(string value)
        {
            LastName = await _service.PostAsync(context => 
            {
                _inner.LastName = value;
                return _inner.LastName;
            });
        }


        public string MiddleName { get; private set; }
        public async Task SetMiddleNameAsync(string value)
        {
            MiddleName = await _service.PostAsync(context => 
            {
                _inner.MiddleName = value;
                return _inner.MiddleName;
            });
        }


        public string PrimaryOncologistId { get; private set; }


        public string PrimaryOncologistName { get; private set; }


        public async Task<IReadOnlyList<IReferencePoint>> GetReferencePointsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.ReferencePoints?.Select(x => new AsyncReferencePoint(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IRegistration>> GetRegistrationsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Registrations?.Select(x => new AsyncRegistration(x, _service)).ToList());
        }


        public string Sex { get; private set; }


        public string SSN { get; private set; }


        public async Task<IReadOnlyList<IStructureSet>> GetStructureSetsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.StructureSets?.Select(x => new AsyncStructureSet(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IStudy>> GetStudiesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Studies?.Select(x => new AsyncStudy(x, _service)).ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Patient> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Patient, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            CreationDateTime = _inner.CreationDateTime;
            DateOfBirth = _inner.DateOfBirth;
            FirstName = _inner.FirstName;
            HasModifiedData = _inner.HasModifiedData;
            Id2 = _inner.Id2;
            LastName = _inner.LastName;
            MiddleName = _inner.MiddleName;
            PrimaryOncologistId = _inner.PrimaryOncologistId;
            PrimaryOncologistName = _inner.PrimaryOncologistName;
            Sex = _inner.Sex;
            SSN = _inner.SSN;
        }

        public static implicit operator VMS.TPS.Common.Model.API.Patient(AsyncPatient wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Patient IEsapiWrapper<VMS.TPS.Common.Model.API.Patient>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Patient>.Service => _service;
    }
}

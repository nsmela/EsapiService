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
            _inner = inner;
            _service = service;

            Courses = inner.Courses;
            CreationDateTime = inner.CreationDateTime;
            DateOfBirth = inner.DateOfBirth;
            Departments = inner.Departments;
            FirstName = inner.FirstName;
            HasModifiedData = inner.HasModifiedData;
            Id2 = inner.Id2;
            LastName = inner.LastName;
            MiddleName = inner.MiddleName;
            PrimaryOncologistId = inner.PrimaryOncologistId;
            PrimaryOncologistName = inner.PrimaryOncologistName;
            ReferencePoints = inner.ReferencePoints;
            Registrations = inner.Registrations;
            Sex = inner.Sex;
            SSN = inner.SSN;
            StructureSets = inner.StructureSets;
            Studies = inner.Studies;
        }

        public async Task<ICourse> AddCourseAsync()
        {
            return await _service.PostAsync(context => 
                _inner.AddCourse() is var result && result is null ? null : new AsyncCourse(result, _service));
        }


        public async Task<IReferencePoint> AddReferencePointAsync(bool target, string id)
        {
            return await _service.PostAsync(context => 
                _inner.AddReferencePoint(target, id) is var result && result is null ? null : new AsyncReferencePoint(result, _service));
        }


        // Simple Void Method
        public Task BeginModificationsAsync() => _service.PostAsync(context => _inner.BeginModifications());

        // Simple Method
        public Task<bool> CanAddCourseAsync() => _service.PostAsync(context => _inner.CanAddCourse());

        public async Task<(bool result, string errorMessage)> CanAddEmptyPhantomAsync()
        {
            var postResult = await _service.PostAsync(context => {
                string errorMessage_temp = default(string);
                var result = _inner.CanAddEmptyPhantom(out errorMessage_temp);
                return (result, errorMessage_temp);
            });
            return (postResult);
        }

        public async Task<(bool result, string errorMessage)> CanCopyImageFromOtherPatientAsync(IStudy targetStudy, string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId)
        {
            var postResult = await _service.PostAsync(context => {
                var value = ((AsyncStudy)targetStudy)._inner;
                string errorMessage_temp = default(string);
                var result = _inner.CanCopyImageFromOtherPatient(value, otherPatientId, otherPatientStudyId, otherPatient3DImageId, out errorMessage_temp);
                return (result, errorMessage_temp);
            });
            return (postResult);
        }

        // Simple Method
        public Task<bool> CanModifyDataAsync() => _service.PostAsync(context => _inner.CanModifyData());

        // Simple Method
        public Task<bool> CanRemoveCourseAsync(ICourse course) => _service.PostAsync(context => _inner.CanRemoveCourse(((AsyncCourse)course)._inner));

        public async Task<(bool result, string errorMessage)> CanRemoveEmptyPhantomAsync(IStructureSet structureset)
        {
            var postResult = await _service.PostAsync(context => {
                var value = ((AsyncStructureSet)structureset)._inner;
                string errorMessage_temp = default(string);
                var result = _inner.CanRemoveEmptyPhantom(value, out errorMessage_temp);
                return (result, errorMessage_temp);
            });
            return (postResult);
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
        public Task RemoveCourseAsync(ICourse course) => _service.PostAsync(context => _inner.RemoveCourse(((AsyncCourse)course)._inner));

        // Simple Void Method
        public Task RemoveEmptyPhantomAsync(IStructureSet structureset) => _service.PostAsync(context => _inner.RemoveEmptyPhantom(((AsyncStructureSet)structureset)._inner));

        public IEnumerable<Course> Courses { get; }

        public DateTime? CreationDateTime { get; }

        public DateTime? DateOfBirth { get; }

        public async Task<IDepartment> GetDefaultDepartmentAsync()
        {
            return await _service.PostAsync(context => 
                _inner.DefaultDepartment is null ? null : new AsyncDepartment(_inner.DefaultDepartment, _service));
        }

        public IEnumerable<Department> Departments { get; }

        public string FirstName { get; private set; }
        public async Task SetFirstNameAsync(string value)
        {
            FirstName = await _service.PostAsync(context => 
            {
                _inner.FirstName = value;
                return _inner.FirstName;
            });
        }

        public bool HasModifiedData { get; }

        public async Task<IHospital> GetHospitalAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Hospital is null ? null : new AsyncHospital(_inner.Hospital, _service));
        }

        public string Id2 { get; }

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

        public string PrimaryOncologistId { get; }

        public string PrimaryOncologistName { get; }

        public IEnumerable<ReferencePoint> ReferencePoints { get; }

        public IEnumerable<Registration> Registrations { get; }

        public string Sex { get; }

        public string SSN { get; }

        public IEnumerable<StructureSet> StructureSets { get; }

        public IEnumerable<Study> Studies { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Patient> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Patient, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Patient(AsyncPatient wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Patient IEsapiWrapper<VMS.TPS.Common.Model.API.Patient>.Inner => _inner;
    }
}

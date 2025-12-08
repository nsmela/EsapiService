using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncPatient : IPatient
    {
        internal readonly VMS.TPS.Common.Model.API.Patient _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncPatient(VMS.TPS.Common.Model.API.Patient inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

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
            return await _service.RunAsync(() => 
                _inner.AddCourse() is var result && result is null ? null : new AsyncCourse(result, _service));
        }


        public async Task<IStructureSet> AddEmptyPhantomAsync(string imageId, PatientOrientation orientation, int xSizePixel, int ySizePixel, double widthMM, double heightMM, int nrOfPlanes, double planeSepMM)
        {
            return await _service.RunAsync(() => 
                _inner.AddEmptyPhantom(imageId, orientation, xSizePixel, ySizePixel, widthMM, heightMM, nrOfPlanes, planeSepMM) is var result && result is null ? null : new AsyncStructureSet(result, _service));
        }


        public async Task<IReferencePoint> AddReferencePointAsync(bool target, string id)
        {
            return await _service.RunAsync(() => 
                _inner.AddReferencePoint(target, id) is var result && result is null ? null : new AsyncReferencePoint(result, _service));
        }


        public Task BeginModificationsAsync() => _service.RunAsync(() => _inner.BeginModifications());

        public Task<bool> CanAddCourseAsync() => _service.RunAsync(() => _inner.CanAddCourse());

        public async Task<(bool Result, string errorMessage)> CanAddEmptyPhantomAsync()
        {
            string errorMessage_temp;
            var result = await _service.RunAsync(() => _inner.CanAddEmptyPhantom(out errorMessage_temp));
            return (result, errorMessage_temp);
        }

        public async Task<(bool Result, string errorMessage)> CanCopyImageFromOtherPatientAsync(IStudy targetStudy, string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId)
        {
            string errorMessage_temp;
            var result = await _service.RunAsync(() => _inner.CanCopyImageFromOtherPatient(targetStudy._inner, otherPatientId, otherPatientStudyId, otherPatient3DImageId, out errorMessage_temp));
            return (result, errorMessage_temp);
        }

        public Task<bool> CanModifyDataAsync() => _service.RunAsync(() => _inner.CanModifyData());

        public Task<bool> CanRemoveCourseAsync(ICourse course) => _service.RunAsync(() => _inner.CanRemoveCourse(course));

        public async Task<(bool Result, string errorMessage)> CanRemoveEmptyPhantomAsync(IStructureSet structureset)
        {
            string errorMessage_temp;
            var result = await _service.RunAsync(() => _inner.CanRemoveEmptyPhantom(structureset._inner, out errorMessage_temp));
            return (result, errorMessage_temp);
        }

        public async Task<IStructureSet> CopyImageFromOtherPatientAsync(string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId)
        {
            return await _service.RunAsync(() => 
                _inner.CopyImageFromOtherPatient(otherPatientId, otherPatientStudyId, otherPatient3DImageId) is var result && result is null ? null : new AsyncStructureSet(result, _service));
        }


        public async Task<IStructureSet> CopyImageFromOtherPatientAsync(IStudy targetStudy, string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId)
        {
            return await _service.RunAsync(() => 
                _inner.CopyImageFromOtherPatient(targetStudy, otherPatientId, otherPatientStudyId, otherPatient3DImageId) is var result && result is null ? null : new AsyncStructureSet(result, _service));
        }


        public Task RemoveCourseAsync(ICourse course) => _service.RunAsync(() => _inner.RemoveCourse(course));

        public Task RemoveEmptyPhantomAsync(IStructureSet structureset) => _service.RunAsync(() => _inner.RemoveEmptyPhantom(structureset));

        public async Task<IReadOnlyList<ICourse>> GetCoursesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Courses?.Select(x => new AsyncCourse(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<DateTime>> GetCreationDateTimeAsync()
        {
            return await _service.RunAsync(() => _inner.CreationDateTime?.ToList());
        }


        public async Task<IReadOnlyList<DateTime>> GetDateOfBirthAsync()
        {
            return await _service.RunAsync(() => _inner.DateOfBirth?.ToList());
        }


        public async Task<IDepartment> GetDefaultDepartmentAsync()
        {
            return await _service.RunAsync(() => 
                _inner.DefaultDepartment is null ? null : new AsyncDepartment(_inner.DefaultDepartment, _service));
        }

        public async Task<IReadOnlyList<IDepartment>> GetDepartmentsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Departments?.Select(x => new AsyncDepartment(x, _service)).ToList());
        }


        public string FirstName { get; private set; }
        public async Task SetFirstNameAsync(string value)
        {
            FirstName = await _service.RunAsync(() =>
            {
                _inner.FirstName = value;
                return _inner.FirstName;
            });
        }

        public bool HasModifiedData { get; }

        public async Task<IHospital> GetHospitalAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Hospital is null ? null : new AsyncHospital(_inner.Hospital, _service));
        }

        public string Id2 { get; }

        public string LastName { get; private set; }
        public async Task SetLastNameAsync(string value)
        {
            LastName = await _service.RunAsync(() =>
            {
                _inner.LastName = value;
                return _inner.LastName;
            });
        }

        public string MiddleName { get; private set; }
        public async Task SetMiddleNameAsync(string value)
        {
            MiddleName = await _service.RunAsync(() =>
            {
                _inner.MiddleName = value;
                return _inner.MiddleName;
            });
        }

        public string PrimaryOncologistId { get; }

        public string PrimaryOncologistName { get; }

        public async Task<IReadOnlyList<IReferencePoint>> GetReferencePointsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.ReferencePoints?.Select(x => new AsyncReferencePoint(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IRegistration>> GetRegistrationsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Registrations?.Select(x => new AsyncRegistration(x, _service)).ToList());
        }


        public string Sex { get; }

        public string SSN { get; }

        public async Task<IReadOnlyList<IStructureSet>> GetStructureSetsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.StructureSets?.Select(x => new AsyncStructureSet(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IStudy>> GetStudiesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Studies?.Select(x => new AsyncStudy(x, _service)).ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Patient> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Patient, T> func) => _service.RunAsync(() => func(_inner));
    }
}

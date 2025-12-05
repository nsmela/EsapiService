    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
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

            HasModifiedData = inner.HasModifiedData;
            Id2 = inner.Id2;
            PrimaryOncologistId = inner.PrimaryOncologistId;
            PrimaryOncologistName = inner.PrimaryOncologistName;
            Sex = inner.Sex;
            SSN = inner.SSN;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public ICourse AddCourse() => _inner.AddCourse() is var result && result is null ? null : new AsyncCourse(result, _service);
        public IStructureSet AddEmptyPhantom(string imageId, VMS.TPS.Common.Model.Types.PatientOrientation orientation, int xSizePixel, int ySizePixel, double widthMM, double heightMM, int nrOfPlanes, double planeSepMM) => _inner.AddEmptyPhantom(imageId, orientation, xSizePixel, ySizePixel, widthMM, heightMM, nrOfPlanes, planeSepMM) is var result && result is null ? null : new AsyncStructureSet(result, _service);
        public IReferencePoint AddReferencePoint(bool target, string id) => _inner.AddReferencePoint(target, id) is var result && result is null ? null : new AsyncReferencePoint(result, _service);
        public void BeginModifications() => _inner.BeginModifications();
        public bool CanAddCourse() => _inner.CanAddCourse();
        public async System.Threading.Tasks.Task<(bool Result, string errorMessage)> CanAddEmptyPhantomAsync()
        {
            string errorMessage_temp;
            var result = await _service.RunAsync(() => _inner.CanAddEmptyPhantom(out errorMessage_temp));
            return (result, errorMessage_temp);
        }
        public async System.Threading.Tasks.Task<(bool Result, string errorMessage)> CanCopyImageFromOtherPatientAsync(IStudy targetStudy, string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId)
        {
            string errorMessage_temp;
            var result = await _service.RunAsync(() => _inner.CanCopyImageFromOtherPatient(targetStudy._inner, otherPatientId, otherPatientStudyId, otherPatient3DImageId, out errorMessage_temp));
            return (result, errorMessage_temp);
        }
        public bool CanModifyData() => _inner.CanModifyData();
        public bool CanRemoveCourse(VMS.TPS.Common.Model.API.Course course) => _inner.CanRemoveCourse(course);
        public async System.Threading.Tasks.Task<(bool Result, string errorMessage)> CanRemoveEmptyPhantomAsync(IStructureSet structureset)
        {
            string errorMessage_temp;
            var result = await _service.RunAsync(() => _inner.CanRemoveEmptyPhantom(structureset._inner, out errorMessage_temp));
            return (result, errorMessage_temp);
        }
        public IStructureSet CopyImageFromOtherPatient(string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId) => _inner.CopyImageFromOtherPatient(otherPatientId, otherPatientStudyId, otherPatient3DImageId) is var result && result is null ? null : new AsyncStructureSet(result, _service);
        public IStructureSet CopyImageFromOtherPatient(VMS.TPS.Common.Model.API.Study targetStudy, string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId) => _inner.CopyImageFromOtherPatient(targetStudy, otherPatientId, otherPatientStudyId, otherPatient3DImageId) is var result && result is null ? null : new AsyncStructureSet(result, _service);
        public void RemoveCourse(VMS.TPS.Common.Model.API.Course course) => _inner.RemoveCourse(course);
        public void RemoveEmptyPhantom(VMS.TPS.Common.Model.API.StructureSet structureset) => _inner.RemoveEmptyPhantom(structureset);
        public System.Collections.Generic.IReadOnlyList<ICourse> Courses => _inner.Courses?.Select(x => new AsyncCourse(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime => _inner.CreationDateTime?.ToList();
        public System.Collections.Generic.IReadOnlyList<System.DateTime> DateOfBirth => _inner.DateOfBirth?.ToList();
        public IDepartment DefaultDepartment => _inner.DefaultDepartment is null ? null : new AsyncDepartment(_inner.DefaultDepartment, _service);

        public System.Collections.Generic.IReadOnlyList<IDepartment> Departments => _inner.Departments?.Select(x => new AsyncDepartment(x, _service)).ToList();
        public string FirstName => _inner.FirstName;
        public async Task SetFirstNameAsync(string value) => _service.RunAsync(() => _inner.FirstName = value);
        public bool HasModifiedData { get; }
        public IHospital Hospital => _inner.Hospital is null ? null : new AsyncHospital(_inner.Hospital, _service);

        public string Id2 { get; }
        public string LastName => _inner.LastName;
        public async Task SetLastNameAsync(string value) => _service.RunAsync(() => _inner.LastName = value);
        public string MiddleName => _inner.MiddleName;
        public async Task SetMiddleNameAsync(string value) => _service.RunAsync(() => _inner.MiddleName = value);
        public string PrimaryOncologistId { get; }
        public string PrimaryOncologistName { get; }
        public System.Collections.Generic.IReadOnlyList<IReferencePoint> ReferencePoints => _inner.ReferencePoints?.Select(x => new AsyncReferencePoint(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<IRegistration> Registrations => _inner.Registrations?.Select(x => new AsyncRegistration(x, _service)).ToList();
        public string Sex { get; }
        public string SSN { get; }
        public System.Collections.Generic.IReadOnlyList<IStructureSet> StructureSets => _inner.StructureSets?.Select(x => new AsyncStructureSet(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<IStudy> Studies => _inner.Studies?.Select(x => new AsyncStudy(x, _service)).ToList();
    }
}

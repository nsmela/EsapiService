namespace VMS.TPS.Common.Model.API
{
    public interface IPatient : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        ICourse AddCourse();
        IStructureSet AddEmptyPhantom(string imageId, VMS.TPS.Common.Model.Types.PatientOrientation orientation, int xSizePixel, int ySizePixel, double widthMM, double heightMM, int nrOfPlanes, double planeSepMM);
        IReferencePoint AddReferencePoint(bool target, string id);
        void BeginModifications();
        bool CanAddCourse();


        bool CanModifyData();
        bool CanRemoveCourse(VMS.TPS.Common.Model.API.Course course);

        IStructureSet CopyImageFromOtherPatient(string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId);
        IStructureSet CopyImageFromOtherPatient(VMS.TPS.Common.Model.API.Study targetStudy, string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId);
        void RemoveCourse(VMS.TPS.Common.Model.API.Course course);
        void RemoveEmptyPhantom(VMS.TPS.Common.Model.API.StructureSet structureset);
        System.Collections.Generic.IReadOnlyList<ICourse> Courses { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> DateOfBirth { get; }
        IDepartment DefaultDepartment { get; }
        System.Collections.Generic.IReadOnlyList<IDepartment> Departments { get; }
        string FirstName { get; }
        System.Threading.Tasks.Task SetFirstNameAsync(string value);
        bool HasModifiedData { get; }
        IHospital Hospital { get; }
        string Id2 { get; }
        string LastName { get; }
        System.Threading.Tasks.Task SetLastNameAsync(string value);
        string MiddleName { get; }
        System.Threading.Tasks.Task SetMiddleNameAsync(string value);
        string PrimaryOncologistId { get; }
        string PrimaryOncologistName { get; }
        System.Collections.Generic.IReadOnlyList<IReferencePoint> ReferencePoints { get; }
        System.Collections.Generic.IReadOnlyList<IRegistration> Registrations { get; }
        string Sex { get; }
        string SSN { get; }
        System.Collections.Generic.IReadOnlyList<IStructureSet> StructureSets { get; }
        System.Collections.Generic.IReadOnlyList<IStudy> Studies { get; }
    }
}

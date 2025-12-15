using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Patient : ApiDataObject
    {
        public Patient()
        {
            Courses = new List<Course>();
            ReferencePoints = new List<ReferencePoint>();
            Registrations = new List<Registration>();
            StructureSets = new List<StructureSet>();
            Studies = new List<Study>();
        }

        public Course AddCourse() => default;
        public StructureSet AddEmptyPhantom(string imageId, PatientOrientation orientation, int xSizePixel, int ySizePixel, double widthMM, double heightMM, int nrOfPlanes, double planeSepMM) => default;
        public ReferencePoint AddReferencePoint(bool target, string id) => default;
        public void BeginModifications() { }
        public bool CanAddCourse() => default;
        public bool CanAddEmptyPhantom(out string errorMessage)
        {
            errorMessage = default;
            return default;
        }

        public bool CanCopyImageFromOtherPatient(Study targetStudy, string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId, out string errorMessage)
        {
            errorMessage = default;
            return default;
        }

        public bool CanModifyData() => default;
        public bool CanRemoveCourse(Course course) => default;
        public bool CanRemoveEmptyPhantom(StructureSet structureset, out string errorMessage)
        {
            errorMessage = default;
            return default;
        }

        public StructureSet CopyImageFromOtherPatient(string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId) => default;
        public StructureSet CopyImageFromOtherPatient(Study targetStudy, string otherPatientId, string otherPatientStudyId, string otherPatient3DImageId) => default;
        public void RemoveCourse(Course course) { }
        public void RemoveEmptyPhantom(StructureSet structureset) { }
        public IEnumerable<Course> Courses { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string DefaultDepartment { get; set; }
        public string FirstName { get; set; }
        public bool HasModifiedData { get; set; }
        public Hospital Hospital { get; set; }
        public string Id2 { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PrimaryOncologistId { get; set; }
        public string PrimaryOncologistName { get; set; }
        public IEnumerable<ReferencePoint> ReferencePoints { get; set; }
        public IEnumerable<Registration> Registrations { get; set; }
        public string Sex { get; set; }
        public string SSN { get; set; }
        public IEnumerable<StructureSet> StructureSets { get; set; }
        public IEnumerable<Study> Studies { get; set; }
    }
}

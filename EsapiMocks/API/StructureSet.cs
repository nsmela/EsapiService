using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class StructureSet : ApiDataObject
    {
        public StructureSet()
        {
            Structures = new List<Structure>();
            ApplicationScriptLogs = new List<ApplicationScriptLog>();
        }

        public bool AddCouchStructures(string couchModel, PatientOrientation orientation, RailPosition railA, RailPosition railB, double? surfaceHU, double? interiorHU, double? railHU, out IReadOnlyList<Structure> addedStructures, out bool imageResized, out string error)
        {
            addedStructures = default;
            imageResized = default;
            error = default;
            return default;
        }

        public bool RemoveCouchStructures(out IReadOnlyList<string> removedStructureIds, out string error)
        {
            removedStructureIds = default;
            error = default;
            return default;
        }

        public Structure AddReferenceLine(string name, string id, VVector[] referenceLinePoints) => default;
        public Structure AddStructure(string dicomType, string id) => default;
        public Structure AddStructure(StructureCodeInfo code) => default;
        public bool CanAddCouchStructures(out string error)
        {
            error = default;
            return default;
        }

        public bool CanAddStructure(string dicomType, string id) => default;
        public bool CanRemoveCouchStructures(out string error)
        {
            error = default;
            return default;
        }

        public bool CanRemoveStructure(Structure structure) => default;
        public StructureSet Copy() => default;
        public Structure CreateAndSearchBody(SearchBodyParameters parameters) => default;
        public void Delete() { }
        public SearchBodyParameters GetDefaultSearchBodyParameters() => default;
        public void RemoveStructure(Structure structure) { }
        public IEnumerable<Structure> Structures { get; set; }
        public IEnumerable<ApplicationScriptLog> ApplicationScriptLogs { get; set; }
        public Image Image { get; set; }
        public Patient Patient { get; set; }
        public Series Series { get; set; }
        public string SeriesUID { get; set; }
        public string UID { get; set; }

        /* --- Skipped Members (Not generated) ---
           - Id: Shadows base member in wrapped base class
           - Name: Shadows base member in wrapped base class
           - Comment: Shadows base member in wrapped base class
        */
    }
}

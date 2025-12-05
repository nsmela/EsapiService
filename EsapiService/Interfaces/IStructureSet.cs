namespace VMS.TPS.Common.Model.API
{
    public interface IStructureSet : IApiDataObject
    {


        void WriteXml(System.Xml.XmlWriter writer);
        IStructure AddReferenceLine(string name, string id, VMS.TPS.Common.Model.Types.VVector[] referenceLinePoints);
        IStructure AddStructure(string dicomType, string id);
        IStructure AddStructure(VMS.TPS.Common.Model.Types.StructureCodeInfo code);

        bool CanAddStructure(string dicomType, string id);

        bool CanRemoveStructure(VMS.TPS.Common.Model.API.Structure structure);
        IStructureSet Copy();
        IStructure CreateAndSearchBody(VMS.TPS.Common.Model.API.SearchBodyParameters parameters);
        void Delete();
        ISearchBodyParameters GetDefaultSearchBodyParameters();
        void RemoveStructure(VMS.TPS.Common.Model.API.Structure structure);
        System.Collections.Generic.IReadOnlyList<IStructure> Structures { get; }
        string Id { get; }
        System.Threading.Tasks.Task SetIdAsync(string value);
        string Name { get; }
        System.Threading.Tasks.Task SetNameAsync(string value);
        string Comment { get; }
        System.Threading.Tasks.Task SetCommentAsync(string value);
        System.Collections.Generic.IReadOnlyList<IApplicationScriptLog> ApplicationScriptLogs { get; }
        IImage Image { get; }
        IPatient Patient { get; }
        ISeries Series { get; }
        string SeriesUID { get; }
        string UID { get; }
    }
}

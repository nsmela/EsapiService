namespace VMS.TPS.Common.Model.API
{
    public interface IIsodose : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        System.Windows.Media.Color Color { get; }
        VMS.TPS.Common.Model.Types.DoseValue Level { get; }
        System.Windows.Media.Media3D.MeshGeometry3D MeshGeometry { get; }
    }
}

namespace VMS.TPS.Common.Model.API
{
    public interface IControlPoint : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        IBeam Beam { get; }
        double CollimatorAngle { get; }
        double GantryAngle { get; }
        int Index { get; }
        System.Collections.Generic.IReadOnlyList<double> JawPositions { get; }
        float[,] LeafPositions { get; }
        double MetersetWeight { get; }
        double PatientSupportAngle { get; }
        double TableTopLateralPosition { get; }
        double TableTopLongitudinalPosition { get; }
        double TableTopVerticalPosition { get; }
    }
}

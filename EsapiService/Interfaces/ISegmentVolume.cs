namespace VMS.TPS.Common.Model.API
{
    public interface ISegmentVolume : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        ISegmentVolume And(VMS.TPS.Common.Model.API.SegmentVolume other);
        ISegmentVolume AsymmetricMargin(VMS.TPS.Common.Model.Types.AxisAlignedMargins margins);
        ISegmentVolume Margin(double marginInMM);
        ISegmentVolume Not();
        ISegmentVolume Or(VMS.TPS.Common.Model.API.SegmentVolume other);
        ISegmentVolume Sub(VMS.TPS.Common.Model.API.SegmentVolume other);
        ISegmentVolume Xor(VMS.TPS.Common.Model.API.SegmentVolume other);
    }
}

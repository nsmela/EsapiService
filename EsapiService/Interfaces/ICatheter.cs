namespace VMS.TPS.Common.Model.API
{
    public interface ICatheter : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        double GetSourcePosCenterDistanceFromTip(VMS.TPS.Common.Model.API.SourcePosition sourcePosition);
        double GetTotalDwellTime();
        void LinkRefLine(VMS.TPS.Common.Model.API.Structure refLine);
        void LinkRefPoint(VMS.TPS.Common.Model.API.ReferencePoint refPoint);

        VMS.TPS.Common.Model.Types.SetSourcePositionsResult SetSourcePositions(double stepSize, double firstSourcePosition, double lastSourcePosition);
        void UnlinkRefLine(VMS.TPS.Common.Model.API.Structure refLine);
        void UnlinkRefPoint(VMS.TPS.Common.Model.API.ReferencePoint refPoint);
        double ApplicatorLength { get; }
        System.Threading.Tasks.Task SetApplicatorLengthAsync(double value);
        System.Collections.Generic.IReadOnlyList<IBrachyFieldReferencePoint> BrachyFieldReferencePoints { get; }
        int BrachySolidApplicatorPartID { get; }
        int ChannelNumber { get; }
        System.Threading.Tasks.Task SetChannelNumberAsync(int value);
        System.Windows.Media.Color Color { get; }
        double DeadSpaceLength { get; }
        System.Threading.Tasks.Task SetDeadSpaceLengthAsync(double value);
        double FirstSourcePosition { get; }
        int GroupNumber { get; }
        double LastSourcePosition { get; }
        VMS.TPS.Common.Model.Types.VVector[] Shape { get; }
        System.Threading.Tasks.Task SetShapeAsync(VMS.TPS.Common.Model.Types.VVector[] value);
        System.Collections.Generic.IReadOnlyList<ISourcePosition> SourcePositions { get; }
        double StepSize { get; }
        IBrachyTreatmentUnit TreatmentUnit { get; }
    }
}

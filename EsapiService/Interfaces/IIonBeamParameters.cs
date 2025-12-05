namespace VMS.TPS.Common.Model.API
{
    public interface IIonBeamParameters : IBeamParameters
    {
        System.Collections.Generic.IReadOnlyList<IIonControlPointParameters> ControlPoints { get; }
        string PreSelectedRangeShifter1Id { get; }
        System.Threading.Tasks.Task SetPreSelectedRangeShifter1IdAsync(string value);
        string PreSelectedRangeShifter1Setting { get; }
        System.Threading.Tasks.Task SetPreSelectedRangeShifter1SettingAsync(string value);
        string PreSelectedRangeShifter2Id { get; }
        System.Threading.Tasks.Task SetPreSelectedRangeShifter2IdAsync(string value);
        string PreSelectedRangeShifter2Setting { get; }
        System.Threading.Tasks.Task SetPreSelectedRangeShifter2SettingAsync(string value);
        IIonControlPointPairCollection IonControlPointPairs { get; }
        string SnoutId { get; }
        double SnoutPosition { get; }
        IStructure TargetStructure { get; }
        System.Threading.Tasks.Task SetTargetStructureAsync(IStructure value);
    }
}

namespace VMS.TPS.Common.Model.API
{
    public interface ISearchBodyParameters : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        void LoadDefaults();
        bool FillAllCavities { get; }
        System.Threading.Tasks.Task SetFillAllCavitiesAsync(bool value);
        bool KeepLargestParts { get; }
        System.Threading.Tasks.Task SetKeepLargestPartsAsync(bool value);
        int LowerHUThreshold { get; }
        System.Threading.Tasks.Task SetLowerHUThresholdAsync(int value);
        int MREdgeThresholdHigh { get; }
        System.Threading.Tasks.Task SetMREdgeThresholdHighAsync(int value);
        int MREdgeThresholdLow { get; }
        System.Threading.Tasks.Task SetMREdgeThresholdLowAsync(int value);
        int NumberOfLargestPartsToKeep { get; }
        System.Threading.Tasks.Task SetNumberOfLargestPartsToKeepAsync(int value);
        bool PreCloseOpenings { get; }
        System.Threading.Tasks.Task SetPreCloseOpeningsAsync(bool value);
        double PreCloseOpeningsRadius { get; }
        System.Threading.Tasks.Task SetPreCloseOpeningsRadiusAsync(double value);
        bool PreDisconnect { get; }
        System.Threading.Tasks.Task SetPreDisconnectAsync(bool value);
        double PreDisconnectRadius { get; }
        System.Threading.Tasks.Task SetPreDisconnectRadiusAsync(double value);
        bool Smoothing { get; }
        System.Threading.Tasks.Task SetSmoothingAsync(bool value);
        int SmoothingLevel { get; }
        System.Threading.Tasks.Task SetSmoothingLevelAsync(int value);
    }
}

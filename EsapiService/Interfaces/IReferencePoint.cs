namespace VMS.TPS.Common.Model.API
{
    public interface IReferencePoint : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        bool AddLocation(VMS.TPS.Common.Model.API.Image Image, double x, double y, double z, System.Text.StringBuilder errorHint);
        bool ChangeLocation(VMS.TPS.Common.Model.API.Image Image, double x, double y, double z, System.Text.StringBuilder errorHint);
        VMS.TPS.Common.Model.Types.VVector GetReferencePointLocation(VMS.TPS.Common.Model.API.Image Image);
        VMS.TPS.Common.Model.Types.VVector GetReferencePointLocation(VMS.TPS.Common.Model.API.PlanSetup planSetup);
        bool HasLocation(VMS.TPS.Common.Model.API.PlanSetup planSetup);
        bool RemoveLocation(VMS.TPS.Common.Model.API.Image Image, System.Text.StringBuilder errorHint);
        string Id { get; }
        System.Threading.Tasks.Task SetIdAsync(string value);
        VMS.TPS.Common.Model.Types.DoseValue DailyDoseLimit { get; }
        System.Threading.Tasks.Task SetDailyDoseLimitAsync(VMS.TPS.Common.Model.Types.DoseValue value);
        VMS.TPS.Common.Model.Types.DoseValue SessionDoseLimit { get; }
        System.Threading.Tasks.Task SetSessionDoseLimitAsync(VMS.TPS.Common.Model.Types.DoseValue value);
        VMS.TPS.Common.Model.Types.DoseValue TotalDoseLimit { get; }
        System.Threading.Tasks.Task SetTotalDoseLimitAsync(VMS.TPS.Common.Model.Types.DoseValue value);
    }
}

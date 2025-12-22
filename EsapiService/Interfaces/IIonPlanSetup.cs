using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;

namespace Esapi.Interfaces
{
    public interface IIonPlanSetup : IPlanSetup
    {
        // --- Simple Properties --- //
        bool IsPostProcessingNeeded { get; } // simple property
        Task SetIsPostProcessingNeededAsync(bool value);

        // --- Accessors --- //
        Task<IEvaluationDose> GetDoseAsEvaluationDoseAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<IIonBeam>> GetIonBeamsAsync(); // collection proeprty context

        // --- Methods --- //
        Task<ICalculationResult> CalculateDoseAsync(); // complex method
        Task<ICalculationResult> PostProcessAndCalculateDoseAsync(); // complex method
        Task<ICalculationResult> CalculateDoseWithoutPostProcessingAsync(); // complex method
        Task<IReadOnlyList<string>> GetModelsForCalculationTypeAsync(CalculationType calculationType); // simple collection method 
        Task<IEvaluationDose> CopyEvaluationDoseAsync(IDose existing); // complex method
        Task<IEvaluationDose> CreateEvaluationDoseAsync(); // complex method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonPlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.IonPlanSetup> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonPlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonPlanSetup, T> func);
    }
}

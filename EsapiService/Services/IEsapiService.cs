using Esapi.Interfaces;
using System;
using System.Threading.Tasks;

namespace Esapi.Services {
    /// <summary>
    /// The core dispatcher service. This is the "post office"
    /// that takes work from the UI thread and sends it to the ESAPI thread.
    /// </summary>
    public interface IEsapiService {
        /// <summary>
        /// Posts a function to be executed on the ESAPI thread
        /// and returns a Task that completes with the result.
        /// </summary>
        /// <typeparam name="T">The return type</typeparam>
        /// <param name="work">The function to execute</param>
        /// <returns>A Task for the result</returns>
        Task<T> PostAsync<T>(Func<IEsapiContext, T> work);

        /// <summary>
        /// Posts an action to be executed on the ESAPI thread
        /// and returns a Task that completes when it's done.
        /// </summary>
        /// <param name="work">The action to execute</param>
        /// <returns>A Task that completes on finish</returns>
        Task PostAsync(Action<IEsapiContext> work);

        /// <summary>
        /// Special-case helper to get the patient.
        /// In a real plugin, the context already has the patient.
        /// In a standalone app, we'd need to open one.
        /// This simplifies the test.
        /// </summary>
        Task<IPatient> GetPatientAsync();

        /// <summary>
        /// Special-case helper to get the current plan.
        /// In a real plugin, the context already has the patient.
        /// In a standalone app, we'd need to open one.
        /// This simplifies the test.
        /// </summary>
        Task<IPlanSetup> GetPlanAsync();
    }
}

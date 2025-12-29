using Esapi.Interfaces;
using Esapi.Wrappers;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Esapi.Services {
    /// <summary>
    /// The core dispatcher service. This is the "post office"
    /// that takes work from the UI thread and sends it to the ESAPI thread.
    /// </summary>
    public interface IEsapiService
    {
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
        /// Returns true if a patient is currently open.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsPatientOpen();

        /// <summary>
        /// Opens and returns a patient if the id is known.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IPatient> OpenPatientByIdAsync(string id);

        /// <summary>
        /// Special-case helper to get the current plan.
        /// In a real plugin, the context already has the patient.
        /// In a standalone app, we'd need to open one.
        /// This simplifies the test.
        /// </summary>
        Task<IPlanSetup> GetPlanAsync();

        Task BeginModificationsAsync();

        Task SavePatientAsync();

        Task ClosePatient();
    }

    /// <summary>
    /// This is the "Post Office" for the "Async Proxy" pattern.
    /// It uses a BlockingCollection (mailbox)
    /// to send work to the ESAPI thread.
    /// </summary>
    public class EsapiService : IEsapiService {
        private readonly BlockingCollection<IActorMessage> _mailbox;

        public EsapiService(BlockingCollection<IActorMessage> mailbox) {
            _mailbox = mailbox;
        }

        /// <summary>
        /// Posts a task to be executed asynchronously within the ESAPI plugin context.
        /// </summary>
        /// <remarks>The provided function is executed asynchronously, and the result is encapsulated in a
        /// task. This method is useful for performing operations that require the ESAPI plugin context.</remarks>
        /// <typeparam name="T">The type of the result produced by the task.</typeparam>
        /// <param name="work">A function that defines the work to be performed, taking an <see cref="IEsapiContext"/> as input and
        /// returning a result of type <typeparamref name="T"/>.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, containing the result of type
        /// <typeparamref name="T"/>.</returns>
        public Task<T> PostAsync<T>(Func<IEsapiContext, T> work) {
            var tcs = new TaskCompletionSource<T>();
            var message = new ActorMessage<T>(work, tcs);
            _mailbox.Add(message);
            return tcs.Task;
        }

        /// <summary>
        /// Asynchronously posts a task to be executed within the ESAPI plugin context.
        /// </summary>
        /// <param name="work">The action to be executed, which receives an <see cref="IEsapiPluginContext"/> as its parameter.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task completes when the action has been
        /// executed.</returns>
        public Task PostAsync(Action<IEsapiContext> work) {
            var tcs = new TaskCompletionSource<object>();
            var message = new ActorActionMessage(work, tcs);
            _mailbox.Add(message);
            return tcs.Task;
        }

        // --- Public Facade Methods ---

        /// <summary>
        /// Asynchronously retrieves the current patient information.
        /// </summary>
        /// <remarks>This method must be called from a context where the ESAPI thread is available, as it
        /// interacts with the ESAPI patient data.</remarks>
        /// <returns>An <see cref="IAsyncPatient"/> representing the current patient. The returned object provides asynchronous
        /// access to patient data.</returns>
        public async Task<IPatient> GetPatientAsync() {
            // Note: Returning a AsyncPatient to a Task<IAsyncPatient>
            // runs into a covariance issue, so we must perform the operation
            // as such to avoid the issue.

            // 1. We 'await' the Task<AsyncPatient>
            AsyncPatient patientWrapper = await PostAsync(context => {
                // This runs on the ESAPI thread
                return new AsyncPatient(context.Patient, this);
            });

            // 2. We return the concrete wrapper, which is
            //    implicitly cast to the interface.
            return patientWrapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsPatientOpen() =>
            await PostAsync(context => context.Patient != null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<IPatient> OpenPatientByIdAsync(string id) {
            // Note: Returning a AsyncPatient to a Task<IAsyncPatient>
            // runs into a covariance issue, so we must perform the operation
            // as such to avoid the issue.
            // 1. We 'await' the Task<AsyncPatient>
            AsyncPatient patientWrapper = await PostAsync(context => {
                // This runs on the ESAPI thread
                var appContext = context as IEsapiAppContext;
                if (appContext is null)
                {
                    throw new InvalidOperationException("Cannot open patient: context is not IEsapiAppContext.");
                }
                var patient = appContext.App.OpenPatientById(id);
                if (patient is null)
                {
                    throw new InvalidOperationException($"No patient found with ID '{id}'.");
                }
                return new AsyncPatient(patient, this);
            });
            // 2. We return the concrete wrapper, which is
            //    implicitly cast to the interface.
            return patientWrapper;
        }

        /// <summary>
        /// Asynchronously retrieves the current plan setup for the patient.
        /// </summary>
        /// <remarks>This method executes on the ESAPI thread and returns an <see cref="IAsyncPlanSetup"/>
        /// representing the current plan setup. If no plan setup is found, an exception is thrown.</remarks>
        /// <returns>An <see cref="IAsyncPlanSetup"/> representing the current plan setup for the patient.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no current plan setup is found for the patient.</exception>
        public async Task<IPlanSetup> GetPlanAsync() {
            // Note: Returning a AsyncPlanSetup to a Task<IAsyncPlanSetup>
            // runs into a covariance issue, so we must perform the operation
            // as such to avoid the issue.

            // 1. We 'await' the Task<AsyncPlanSetup>
            AsyncPlanSetup planWrapper = await PostAsync(context => {
                // This runs on the ESAPI thread
                var plan = context.Plan;
                if (plan is null) {
                    throw new InvalidOperationException("No current plan setup found for the patient.");
                }
                return new AsyncPlanSetup(plan, this);
            });
            // 2. We return the concrete wrapper, which is
            //    implicitly cast to the interface.
            return planWrapper;
        }

        public Task BeginModificationsAsync()
        {
            return PostAsync(context =>
            {
                if (context.Patient is null)
                {
                    // Optional: Log warning
                    return;
                }

                // Varian requires this call before ANY 'set' property is touched
                context.Patient.BeginModifications();
            });
        }

        public Task SavePatientAsync()
        {
            return PostAsync(context =>
            {
                var standalone = context as IEsapiAppContext;

                // Guard clauses 
                if (standalone is null)
                {
                    // In Plugin mode, changes are auto-saved.
                    throw new NotSupportedException("'SavePatient' is only supported in Standalone mode.");
                }

                if (standalone.App is null)
                {
                    throw new InvalidOperationException("Cannot save: App is null. (How did this happen!?)");
                }

                if (standalone.Patient is null)
                {
                    throw new InvalidOperationException("Cannot save: Patient is null.");
                }

                // In Standalone mode, changes are NOT saved unless you call this.
                standalone.App.SaveModifications();
            });
        }

        public Task ClosePatient()
        {
            return PostAsync(context =>
            {
                var standalone = context as IEsapiAppContext;
                // Guard clauses 
                if (standalone is null)
                {
                    // In Plugin mode, closing patient is not supported.
                    throw new NotSupportedException("'ClosePatient' is only supported in Standalone mode.");
                }
                if (standalone.App is null)
                {
                    throw new InvalidOperationException("Cannot close: App is null. (How did this happen!?)");
                }
                if (standalone.Patient is null)
                {
                    throw new InvalidOperationException("Cannot close: Patient is null.");
                }
                // Close the patient
                standalone.App.ClosePatient();
            });
        }
    }
}

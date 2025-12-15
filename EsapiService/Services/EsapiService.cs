using Esapi.Interfaces;
using Esapi.Wrappers;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Esapi.Services {
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
    }
}

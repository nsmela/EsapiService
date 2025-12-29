using Esapi.Interfaces;
using Esapi.Services; // Ensure this matches your IEsapiService namespace
using Esapi.Wrappers; // For AsyncPatient, etc.
using System;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace EsapiTestAdapter
{
    // A "Fake" service that runs everything immediately on the current thread.
    public class TestEsapiService : IEsapiService
    {
        private readonly IEsapiContext _context;

        public TestEsapiService(IEsapiContext context)
        {
            _context = context;
        }

        // The Magic: Run immediately, don't queue!
        public Task<T> PostAsync<T>(Func<IEsapiContext, T> action)
        {
            try
            {
                var result = action(_context);
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                return Task.FromException<T>(ex);
            }
        }

        public Task PostAsync(Action<IEsapiContext> action)
        {
            try
            {
                action(_context);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        // ---------------------------------------------------------
        // Implement High-Level Methods (Mimic EsapiService logic)
        // ---------------------------------------------------------

        // Note: You must implement whatever methods are defined in your IEsapiService interface.
        // Since we pass 'this' to the wrappers, subsequent calls use this TestService too.

        public Task<IPatient> GetPatientAsync()
        {
            return PostAsync<IPatient>(ctx => new AsyncPatient(ctx.Patient, this));
        }

        public Task<IPlanSetup> GetPlanAsync()
        {
            return PostAsync<IPlanSetup>(ctx => new AsyncPlanSetup(ctx.Plan, this));
        }

        public Task<bool> IsPatientOpen() => 
            PostAsync(context => context.Patient != null);

        public Task<IPatient> OpenPatientByIdAsync(string id)
        {
            // Note: Returning a AsyncPatient to a Task<IAsyncPatient>
            // runs into a covariance issue, so we must perform the operation
            // as such to avoid the issue.
            // 1. We 'await' the Task<AsyncPatient>
            return PostAsync(context => {
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
                return (IPatient)(new AsyncPatient(patient, this));
            });
        }

        public Task BeginModificationsAsync() =>
            PostAsync(context => context.Patient.BeginModifications());

        public Task SavePatientAsync() => PostAsync(context =>
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

        public Task ClosePatient() => PostAsync(context =>
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

        // ... Implement other interface methods (GetUsers, ClosePatient) as needed ...
    }
}
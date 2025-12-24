using VMS.TPS.Common.Model.API;

namespace Esapi.Services {
    /// <summary>
    /// A simplified context interface to make the service
    /// testable and usable by both plugins and standalone apps.
    /// </summary>
    public interface IEsapiContext {
        /// <summary>
        /// VMS.TPS.Common.Model.API.Patient
        /// </summary>
        Patient Patient { get; }

        /// <summary>
        /// VMS.TPS.Common.Model.API.User
        /// </summary>
        User CurrentUser { get; }

        /// <summary>
        /// VMS.TPS.Common.Model.API.PlanSetup
        /// </summary>
        PlanSetup Plan { get; }

    }

    public interface IEsapiAppContext : IEsapiContext {
        /// <summary>
        /// VMS.TPS.Common.Model.API.Application
        /// </summary>
        Application App { get; }
    }

    /// <summary>
    /// A simple adapter to convert a Varian ESAPI Application into the context needed for EsapiService
    /// </summary>
    public class StandaloneContext : IEsapiAppContext {
        private readonly Application _app;
        private readonly Patient _patient;
        private readonly PlanSetup _plan;

        public StandaloneContext(Application app, Patient patient, PlanSetup plan) {
            _app = app;
            _patient = patient;
            _plan = plan;
        }

        public Application App => _app;

        /// <summary>
        /// VMS.TPS.Common.Model.API.Patient
        /// </summary>
        public Patient Patient => _patient;

        /// <summary>
        /// VMS.TPS.Common.Model.API.User
        /// </summary>
        public User CurrentUser => _app.CurrentUser;

        /// <summary>
        /// VMS.TPS.Common.Model.API.PlanSetup
        /// </summary>
        public PlanSetup Plan => _plan;
    }

    /// <summary>
    /// A simple adapter to convert a Varian ESAPI ScriptContext into the context needed for EsapiService
    /// </summary>
    public class PluginContext : IEsapiContext {
        private readonly ScriptContext _context;
        public PluginContext(ScriptContext context) => _context = context;

        /// <summary>
        /// VMS.TPS.Common.Model.API.Patient
        /// </summary>
        public Patient Patient => _context.Patient;

        /// <summary>
        /// VMS.TPS.Common.Model.API.User
        /// </summary>
        public User CurrentUser => _context.CurrentUser;

        /// <summary>
        /// VMS.TPS.Common.Model.API.PlanSetup
        /// </summary>
        public PlanSetup Plan => _context.PlanSetup;
    }
}

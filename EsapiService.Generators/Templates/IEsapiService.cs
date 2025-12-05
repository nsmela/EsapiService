using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsapiService.Generators.Templates 
{
    public interface IEsapiService {
        /// <summary>
        /// Executes a void action on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action action);

        /// <summary>
        /// Executes a function on the ESAPI thread and returns the result.
        /// </summary>
        Task<T> RunAsync<T>(Func<T> function);
    }
}

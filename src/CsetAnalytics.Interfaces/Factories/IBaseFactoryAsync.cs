using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsetAnalytics.Interfaces.Factories
{
    public interface IBaseFactoryAsync<in TInput, TResult>
    {
        /// <summary>
        /// Convert the specified input of type "TInput" to an object of type "Task&lt;TResult>"
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TResult> CreateAsync(TInput input);

        /// <summary>
        /// Convert the specified IQueryable input of type "TInput" to an IQueryable of type "Task&lt;TResult>"
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        Task<IQueryable<TResult>> CreateAsync(IQueryable<TInput> inputs);
    }

}

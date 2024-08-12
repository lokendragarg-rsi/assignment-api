using Assignment.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Assignment.Framework.Builder.Abstraction
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResponseBuilder<T>
    {
        /// <summary>
        /// Adds the HTTP status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        ResponseBuilder<T> AddHttpStatus(int? status);

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        ResponseBuilder<T> AddMessage(string message);

        /// <summary>
        /// Adds the success data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        ResponseBuilder<T> AddSuccessData(T data);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        APIResponse<T> Build();
    }
}

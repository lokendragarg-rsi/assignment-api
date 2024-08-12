using Assignment.Dto.Dto;
using Assignment.Framework.Builder.Abstraction;
using Assignment.Framework.Builder.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Assignment.Framework.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseBuilder<T> : IResponseBuilder<T>
    {
        private readonly APIResponse<T> _aPIResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseBuilder{T}"/> class.
        /// </summary>
        public ResponseBuilder()
        {
            _aPIResponse = new APIResponse<T>();
        }

        /// <summary>
        /// Adds the status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public ResponseBuilder<T> AddHttpStatus(int? status)
        {
            _aPIResponse.HttpStatus = status;
            return this;
        }

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public ResponseBuilder<T> AddMessage(string message)
        {
            _aPIResponse.Message = message;
            return this;
        }

        /// <summary>
        /// Adds the success data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public ResponseBuilder<T> AddSuccessData(T data)
        {
            _aPIResponse.Data = data;
            this.AddMessage("success");
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        public APIResponse<T> Build()
        {
            _aPIResponse.ApiResponseStatus = (_aPIResponse.Errors.Count == 0) ? ResponseStatus.OK.ToString() : ResponseStatus.Failed.ToString();
            return _aPIResponse;
        }
    }
}

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Planning.Pocker.Api.Test
{
    [DebuggerDisplay("{Data,nq}")]
    [DebuggerNonUserCode]
    [ExcludeFromCodeCoverage]
    public class Response<D>
    {
        public D Data { get; private set; }

        public HttpStatusCode StatusCode { get; private set; }

        public Response(HttpStatusCode statusCode, D data)
        {
            StatusCode = statusCode;
            Data = data;
        }
    }
}

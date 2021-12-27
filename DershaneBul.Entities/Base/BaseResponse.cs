using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace DershaneBul.Entities.Containers.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = false;
            Message = null;
            Details = null;
        }
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Details { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DershaneBul.Entities.Containers.Response;

namespace DershaneBul.Entities.Containers.Request
{
    public class RequestAddress : BaseRequest
    {
        public Guid AddressId { get; set; }
    }
}

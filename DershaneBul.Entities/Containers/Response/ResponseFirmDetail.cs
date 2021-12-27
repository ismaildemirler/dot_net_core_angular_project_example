using System;
using System.Collections.Generic;
using System.Text;

namespace DershaneBul.Entities.Containers.Response
{
    public class ResponseFirmDetail : BaseResponse
    {
        public string FirmName { get; set; }
        public ResponseFirmAddress FirmAddress { get; set; }
    }
}

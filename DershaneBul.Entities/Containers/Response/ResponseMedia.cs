using System;
using System.Collections.Generic;
using System.Text;
using DershaneBul.Entities.Concrete;

namespace DershaneBul.Entities.Containers.Response
{
    public class ResponseMedia:BaseResponse
    {
        public List<Media> Media { get; set; }
    }
}

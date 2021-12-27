using System;
using System.Collections.Generic;
using System.Text;
using DershaneBul.Entities.ComplexType;
using DershaneBul.Entities.Enums;

namespace DershaneBul.Entities.Containers.Response
{
    public class ResponseFirmContactList : BaseResponse
    { 
        public List<FirmContactComplexTypeModel> FirmContactList { get; set; }
    }
}

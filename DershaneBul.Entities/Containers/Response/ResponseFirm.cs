using DershaneBul.Entities.ComplexType;
using System.Collections.Generic;

namespace DershaneBul.Entities.Containers.Response
{
    public class ResponseFirmList: BaseResponse
    {
        public IList<FirmComplexTypeModel> Firms { get; set; }
    }
    public class ResponseFirm : BaseResponse
    {
        public FirmComplexTypeModel Firm { get; set; }
    }
}

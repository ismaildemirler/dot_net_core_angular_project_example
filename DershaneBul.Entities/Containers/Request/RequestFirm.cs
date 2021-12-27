
using DershaneBul.Entities.Containers.Response;
using System;

namespace DershaneBul.Entities.Containers.Request
{
    public class RequestFirm : BaseRequest
    {
        public Guid FirmId { get; set; }
        public Guid ProgramId { get; set; }
        public Guid FirmProgramId { get; set; }
        public int CityId { get; set; }
        public string SearchText { get; set; }
    }
}

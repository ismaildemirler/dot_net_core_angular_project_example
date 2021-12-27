
namespace DershaneBul.Entities.Containers.Response
{
    public class BaseRequest
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public string AdditionalOrderParameter { get; set; } 
        public string OrderBy { get; set; }
    }
}

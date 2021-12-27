using DershaneBul.Entities.Abstract;

namespace DershaneBul.NGWebUI.Models
{
    public class BaseViewModel: IDto
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public string OrderColumnName { get; set; }
        public string OrderDirection { get; set; }
        public string AdditionalOrderParameter { get; set; }
        public string OrderBy { get; set; }
    }
}

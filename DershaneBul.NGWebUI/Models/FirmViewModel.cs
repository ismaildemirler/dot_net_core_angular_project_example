using System;

namespace DershaneBul.NGWebUI.Models
{
    public class FirmViewModel: BaseViewModel
    {
        public Guid FirmId { get; set; }
        public Guid ProgramId { get; set; }
        public Guid FirmProgramId { get; set; }
        public int CityId { get; set; }
        public string SearchText { get; set; }
    }
}

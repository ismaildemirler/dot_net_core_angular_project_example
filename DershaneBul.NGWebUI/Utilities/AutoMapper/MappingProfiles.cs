using AutoMapper;
using DershaneBul.NGWebUI.Models;
using DershaneBul.Entities.Containers.Request;

namespace DershaneBul.NGWebUI.Utilities.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterViewModel, RequestUser>();
            CreateMap<FirmViewModel, RequestFirm>();
            CreateMap<CityViewModel, RequestCity>();
            CreateMap<ProgramViewModel, RequestProgram>();
        }
    }
}

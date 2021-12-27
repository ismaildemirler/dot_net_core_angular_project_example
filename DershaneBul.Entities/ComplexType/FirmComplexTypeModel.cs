using DershaneBul.Entities.Abstract;
using System;

namespace DershaneBul.Entities.ComplexType
{
    public class FirmComplexTypeModel:IBaseEntity
    {
        public Guid FirmId { get; set; }
        public string FirmName { get; set; }
        public string FirmDescription { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Street { get; set; }
        public string AddressDescription { get; set; }
        public string DoorNumber { get; set; }
        public string CityName { get; set; }
        public string TownName { get; set; }
    }
}

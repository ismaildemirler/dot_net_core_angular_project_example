using System;
using System.Collections.Generic;
using System.Text;

namespace DershaneBul.Entities.Containers.Response
{
    public class ResponseFirmAddress:BaseResponse
    {
        public Guid AddressId { get; set; }
        public string CityName { get; set; }
        public string TownName { get; set; }
        public string AdressDescription{ get; set; }
        public string AdressName{ get; set; }
        public string Street{ get; set; }
        public string DoorNumber{ get; set; }
        public string Longtitude { get; set; }
        public string Latitude { get; set; }
    }
}

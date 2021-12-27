
using DershaneBul.Entities.Concrete;
using System.Collections.Generic;

namespace DershaneBul.Entities.Containers.Response
{
    public class ResponseCities: BaseResponse
    {
        public List<City> Cities { get; set; }
    }
}

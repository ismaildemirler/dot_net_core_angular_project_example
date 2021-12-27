using DershaneBul.Entities.Concrete;
using System.Collections.Generic;

namespace DershaneBul.Entities.Containers.Response
{
    public class ResponseProgram: BaseResponse
    {
        public List<Program> Programs { get; set; }
    }
}

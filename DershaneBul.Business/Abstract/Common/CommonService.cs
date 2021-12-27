using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DershaneBul.Entities.Containers.Request;
using DershaneBul.Entities.Containers.Response;

namespace DershaneBul.Business.Abstract.Common
{
    public interface ICommonService
    {
       ResponseMedia GetMediaByFirmAsync(RequestMedia request);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DershaneBul.Business.Abstract.Common;
using DershaneBul.Business.Abstract.Courses;
using DershaneBul.DataAccess.Abstract.Common;
using DershaneBul.DataAccess.Abstract.Firms;
using DershaneBul.Entities.Concrete;
using DershaneBul.Entities.Containers.Request;
using DershaneBul.Entities.Containers.Response;

namespace DershaneBul.Business.Concrete.Common
{
    public class CommonManager : ICommonService
    {
        private readonly ICommonDAL _commonDAL;
        public CommonManager(ICommonDAL commonDAL)
        {
            _commonDAL = commonDAL;
        }

        public ResponseMedia GetMediaByFirmAsync(RequestMedia request)
        {
            return new ResponseMedia
            {
                Success = true,
                Media = new List<Media>()
                {
                    new Media()
                    {
                        FirmId = Guid.NewGuid(),
                        CreationDate = DateTime.Now,
                        MediaId = Guid.NewGuid()
                    }
                }
            };
        }
    }
}

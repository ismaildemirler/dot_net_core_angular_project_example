using System.Collections.Generic;
using DershaneBul.Core.Models.Concrete;
using DershaneBul.Entities.ComplexType;
using DershaneBul.Entities.Containers.Request;
using System.Threading.Tasks;

namespace DershaneBul.DataAccess.Abstract.Firms
{
    public interface IFirmDAL
    {
        Task<PagedList<FirmComplexTypeModel>> GetComplexFirmListByRequestAsync(RequestFirm request);
        Task<FirmComplexTypeModel> GetFirmComplexTypeByRequestAsync(RequestFirm request);
        Task<List<FirmContactComplexTypeModel>> GetFirmContactComplexTypeByRequestAsync(RequestFirm request);
    }
}

using DershaneBul.Entities.Containers.Request;
using DershaneBul.Entities.Containers.Response;
using System.Threading.Tasks;

namespace DershaneBul.Business.Abstract.Courses
{
    public interface IFirmService
    {
        Task<ResponseFirmList> GetFirmListByRequestAsync(RequestFirm request);
        Task<ResponseFirm> GetFirmByRequestAsync(RequestFirm request);
        Task<ResponseFirmContactList> GetFirmContactListByRequestAsync(RequestFirm request);
        Task<ResponseFirmAddress> GetFirmAddressByRequestAsync(RequestAddress request);
    }
}

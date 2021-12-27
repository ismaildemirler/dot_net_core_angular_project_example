using System;
using System.Linq;
using System.Threading.Tasks;
using DershaneBul.Business.Abstract.Courses;
using DershaneBul.DataAccess.Abstract;
using DershaneBul.DataAccess.Abstract.Firms;
using DershaneBul.Entities.Concrete;
using DershaneBul.Entities.Containers.Request;
using DershaneBul.Entities.Containers.Response;

namespace DershaneBul.Business.Concrete.Firms
{
    public class FirmManager : IFirmService
    {
        private readonly IFirmDAL _firmDAL; 
        private readonly IUnitOfWork _unitOfWork;
        public FirmManager(IFirmDAL firmDAL, IUnitOfWork unitOfWork)
        {
            _firmDAL = firmDAL;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ResponseFirmList> GetFirmListByRequestAsync(RequestFirm request)
        {
            var firms = await _firmDAL.GetComplexFirmListByRequestAsync(request);
            return new ResponseFirmList
            {
                Success = true,
                Firms = firms.Items
            };
        }

        public async Task<ResponseFirm> GetFirmByRequestAsync(RequestFirm request)
        {
            var firmDetail = await _firmDAL.GetFirmComplexTypeByRequestAsync(request);
            return new ResponseFirm
            {
                Success = true,
                Firm = firmDetail
            }; 
        }
        public async Task<ResponseFirmContactList> GetFirmContactListByRequestAsync(RequestFirm request)
        {
            var firmContact = await _firmDAL.GetFirmContactComplexTypeByRequestAsync(request);
            return new ResponseFirmContactList
            {
                Success = true,
                FirmContactList = firmContact 
            };
        }

        public async Task<ResponseFirmAddress> GetFirmAddressByRequestAsync(RequestAddress request)
        { 
            //var addressEntity= await _unitOfWork.GetRepository<Address>().FindAsync(request.AddressId);
            var addressEntity = new Address()
            {
                AddressId = Guid.NewGuid(),
                CityId = 1,
                Street = "2",
                Latitude = "2",
                DoorNumber = "2",
                Longtitude = "2",
                AddressName = "ADRESS",
                AddressDescription = "AÇIKLAMA",
                TownId = 2, 
                CreationDate = DateTime.Now,
                StateId = 2,
                UpdateDate = DateTime.Now
            };
            return new ResponseFirmAddress()
            {
                Success=true,
                AddressId = addressEntity.AddressId,
                Street = addressEntity.Street,
                Longtitude = addressEntity.Longtitude,
                DoorNumber = addressEntity.DoorNumber,
                Latitude = addressEntity.Latitude,
                AdressDescription = addressEntity.AddressDescription,
                AdressName = addressEntity.AddressName,
                CityName = addressEntity.CityId.ToString(),  //TODO: Cache'den ID eşleştirmesi yapılacak
                TownName = addressEntity.TownId.ToString()  //TODO: Cache'den ID eşleştirmesi yapılacak
            };
        }
    }
}

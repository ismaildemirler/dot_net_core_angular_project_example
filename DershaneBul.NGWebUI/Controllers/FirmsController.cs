using System;
using System.Threading.Tasks;
using AutoMapper;
using DershaneBul.Business.Abstract.Courses;
using DershaneBul.Core.NetCore.ActionFilters;
using DershaneBul.Entities.Containers.Request;
using DershaneBul.NGWebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DershaneBul.NGWebUI.Controllers
{
    [Produces("application/json")]
    [Route("api/Firms")]
    public class FirmsController : Controller
    {
        private readonly IFirmService _firmService;
        private readonly IMapper _mapper;

        public FirmsController(
            IFirmService firmService,
            IMapper mapper
        )
        {
            _firmService = firmService;
            _mapper = mapper;
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("getfirms")]
        public async Task<IActionResult> GetFirmsAsync([FromBody] FirmViewModel firmViewModel)
        {
            var firmsToFilter = _mapper.Map<RequestFirm>(firmViewModel);
            var firmResponse = await _firmService.GetFirmListByRequestAsync(firmsToFilter);

            if (!firmResponse.Success)
            {
                return BadRequest(firmResponse);
            }

            return Ok(firmResponse);
        }

        [HttpGet("getFirmDetail")]
        public async Task<IActionResult> GetFirmDetailAsync(FirmViewModel firmViewModel)
        {
            var firmsToFilter = _mapper.Map<RequestFirm>(firmViewModel);
            var firmResponse = await _firmService.GetFirmByRequestAsync(firmsToFilter);

            if (!firmResponse.Success)
            {
                return BadRequest(firmResponse);
            }

            return Ok(firmResponse);
        }

        [HttpGet("getFirmContact")]
        public async Task<IActionResult> GetFirmContactAsync(FirmViewModel firmViewModel)
        {
            var firmsToFilter = _mapper.Map<RequestFirm>(firmViewModel);
            var firmContactList = await _firmService.GetFirmContactListByRequestAsync(firmsToFilter);

            if (!firmContactList.Success)
            {
                return BadRequest(firmContactList);
            }
            return Ok(firmContactList);
        }
        [HttpGet("getFirmAddress")]
        public async Task<IActionResult> GetFirmAddresAsync(RequestAddress requestAddress)
        {
            var firmAddress = await _firmService.GetFirmAddressByRequestAsync(requestAddress);

            if (!firmAddress.Success)
            {
                return BadRequest(firmAddress);
            }
        
            return Ok(firmAddress);
        }
        [HttpGet("getFirmProperty")]
        public async Task<IActionResult> GetFirmPropertyAsync(FirmViewModel firmViewModel)
        {
            var firmResponse = new
            {
                description = "Firmaaa"
            };
            return Ok(firmResponse);
        }

        [HttpGet("getFirmMedia")]
        public async Task<IActionResult> GetFirmMediaAsync(FirmViewModel firmViewModel)
        {
            var firmResponse = new
            {
                description = "Firmaaa"
            };
            return Ok(firmResponse);
        }
        [HttpGet("getFirmPropertyMenu")]
        public async Task<IActionResult> GetFirmPropertyMenuAsync(FirmViewModel firmViewModel)
        {
            var firmResponse = new[] {new
            {
                propertyId = "Genel",
                propertyTitle="Genel"
            },
                new
                {
                    propertyId = "KPSS",
                    propertyTitle="KPSS"
                },
                new
                {
                    propertyId = "YDS",
                    propertyTitle="YDS"
                }
            };
            return Ok(firmResponse);
        }
    }
}
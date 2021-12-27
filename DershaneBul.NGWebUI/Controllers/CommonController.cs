using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DershaneBul.Business.Abstract.Common;
using DershaneBul.Business.Abstract.Courses;
using DershaneBul.Core.NetCore.ActionFilters;
using DershaneBul.Entities.Containers.Request;
using DershaneBul.NGWebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DershaneBul.NGWebUI.Controllers
{
    [Route("api/common")]
    public class CommonController : Controller
    {
        private readonly ICommonService _commonService;
        private readonly IMapper _mapper;
        public CommonController(
            ICommonService commonService,
            IMapper mapper
        )
        {
            _commonService = commonService;
            _mapper = mapper;
        }
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("getmedia")]
        public IActionResult GetFirmsAsync()
        { 
            var commonResponse = _commonService.GetMediaByFirmAsync(new RequestMedia());

            if (!commonResponse.Success)
            {
                return BadRequest(commonResponse);
            }
            return Ok(commonResponse);
        }
    }
}
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
    [Route("api/Parameter")]
    public class ParameterController : Controller
    {
        private readonly IParameterService _parameterService;
        private readonly IMapper _mapper;
        public ParameterController(
            IParameterService parameterService,
            IMapper mapper
            )
        {
            _parameterService = parameterService;
            _mapper = mapper;
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("getprograms")]
        public async Task<IActionResult> GetProgramParametersAsync(
            [FromBody] ProgramViewModel programViewModel)
        {
            var modelToFilter = _mapper.Map<RequestProgram>(programViewModel);
            var programResponse = await _parameterService.GetProgramsByRequestAsync(modelToFilter);

            if (!programResponse.Success)
            {
                return BadRequest(programResponse);
            }
            return Ok(programResponse);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("getcities")]
        public async Task<IActionResult> GetCitiesAsync(
            [FromBody] CityViewModel cityViewModel)
        {
            var cityModel = _mapper.Map<RequestCity>(cityViewModel);
            var cityResponse = await _parameterService.GetCitiesByRequestAsync(cityModel);

            if (!cityResponse.Success)
            {
                return BadRequest(cityResponse);
            }
            return Ok(cityResponse);
        }
    }
}
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DershaneBul.Business.Abstract.Courses;
using DershaneBul.Core.Utilities.Helpers;
using DershaneBul.DataAccess.Abstract;
using DershaneBul.Entities.Concrete;
using DershaneBul.Entities.Containers.Request;
using DershaneBul.Entities.Containers.Response;
using Microsoft.EntityFrameworkCore;

namespace DershaneBul.Business.Concrete.Courses
{
    public class ParameterManager : IParameterService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ParameterManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseProgram> GetProgramsByRequestAsync(RequestProgram request)
        {
            Expression<Func<Program, bool>> baseExp = p => p.ProgramId != null;
            if (request.ProgramId != default(Guid))
            {
                baseExp = baseExp.And(p => p.ProgramId == request.ProgramId);
            }

            var programs = await _unitOfWork.GetRepository<Program>().Queryable().Where(baseExp).ToListAsync();

            return new ResponseProgram
            {
                Success = true,
                Programs = programs
            };
        }

        public async Task<ResponseCities> GetCitiesByRequestAsync(RequestCity request)
        {
            Expression<Func<City, bool>> baseExp = p => p.CityId > 0;
            if (request.CityId > 0)
            {
                baseExp = baseExp.And(p => p.CityId == request.CityId);
            }
            if (!string.IsNullOrWhiteSpace(request.CityName))
            {
                baseExp = baseExp.And(p => p.CityName.Contains(request.CityName));
            }

            var cities = await _unitOfWork.GetRepository<City>().Queryable().Where(baseExp).ToListAsync();

            return new ResponseCities
            {
                Success = true,
                Cities = cities
            };
        }
    }
}

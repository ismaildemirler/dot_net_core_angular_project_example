using DershaneBul.Entities.Containers.Request;
using DershaneBul.Entities.Containers.Response;
using System.Threading.Tasks;

namespace DershaneBul.Business.Abstract.Courses
{
    public interface IParameterService
    {
        Task<ResponseProgram> GetProgramsByRequestAsync(RequestProgram request);
        Task<ResponseCities> GetCitiesByRequestAsync(RequestCity request);
    }
}

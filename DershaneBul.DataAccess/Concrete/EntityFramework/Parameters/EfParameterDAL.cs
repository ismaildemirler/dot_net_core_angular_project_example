
using DershaneBul.DataAccess.Abstract.Parameter;

namespace DershaneBul.DataAccess.Concrete.EntityFramework.Parameter
{
    public class EfParameterDAL : IParameterDAL
    {
        DershaneBulDbContext _context;
        public EfParameterDAL(DershaneBulDbContext context)
        {
            _context = context;
        }
    }
}

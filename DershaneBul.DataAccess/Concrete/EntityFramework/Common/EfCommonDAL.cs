using DershaneBul.Core.DataAccess.Concrete.EntityFramework;
using DershaneBul.DataAccess.Abstract.Common;

namespace DershaneBul.DataAccess.Concrete.EntityFramework.Common
{
    public class EfCommonDAL : PureSqlRepository, ICommonDAL
    {
        readonly DershaneBulDbContext _context;
        public EfCommonDAL(DershaneBulDbContext context) : base(context)
        {
            _context = context;
        }
         
    }
}

using DershaneBul.DataAccess.Abstract.Identity;

namespace DershaneBul.DataAccess.Concrete.EntityFramework.Identity
{
    public class EfIdentityDAL: IIdentityDAL 
    {
        DershaneBulDbContext _context;
        public EfIdentityDAL(DershaneBulDbContext context)
        {
            _context = context;            
        }
    }
}

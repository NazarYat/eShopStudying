using eShopStudying.DataAccess.Repository.IRepository;
using eShopStudying.Models;

namespace eShopStudying.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly SQLDBContext _db;
        public ApplicationUserRepository(SQLDBContext db) : base(db)
        {
            _db = db;
        }
    }
}
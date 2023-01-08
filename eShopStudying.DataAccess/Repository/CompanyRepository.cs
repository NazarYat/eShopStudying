using eShopStudying.DataAccess.Repository.IRepository;
using eShopStudying.Models;

namespace eShopStudying.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly SQLDBContext _db;
        public CompanyRepository(SQLDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
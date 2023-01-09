using eShopStudying.DataAccess.Repository.IRepository;
using eShopStudying.Models;

namespace eShopStudying.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly SQLDBContext _db;
        public ShoppingCartRepository(SQLDBContext db) : base(db)
        {
            _db = db;
        }
    }
}
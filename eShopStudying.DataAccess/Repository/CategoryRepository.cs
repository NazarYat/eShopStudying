using eShopStudying.DataAccess.Repository.IRepository;
using eShopStudying.Models;

namespace eShopStudying.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly SQLDBContext _db;
        public CategoryRepository(SQLDBContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
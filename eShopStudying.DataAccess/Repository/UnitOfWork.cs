using eShopStudying.DataAccess.Repository.IRepository;

namespace eShopStudying.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }

        private readonly SQLDBContext _db;

        public UnitOfWork(SQLDBContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
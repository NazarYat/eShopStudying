using eShopStudying.DataAccess.Repository.IRepository;
using eShopStudying.Models;

namespace eShopStudying.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly SQLDBContext _db;
        public ProductRepository(SQLDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var firstOrDafaultProductFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (firstOrDafaultProductFromDb != null)
            {
                firstOrDafaultProductFromDb.Title = obj.Title;
                firstOrDafaultProductFromDb.Author = obj.Author;
                firstOrDafaultProductFromDb.Category = obj.Category;
                firstOrDafaultProductFromDb.CategoryId = obj.CategoryId;
                firstOrDafaultProductFromDb.CoverType = obj.CoverType;
                firstOrDafaultProductFromDb.CoverTypeId = obj.CoverTypeId;
                firstOrDafaultProductFromDb.Description = obj.Description;
                firstOrDafaultProductFromDb.ISBN = obj.ISBN;
                firstOrDafaultProductFromDb.ListPrice = obj.ListPrice;
                firstOrDafaultProductFromDb.Price = obj.Price;
                firstOrDafaultProductFromDb.Price50 = obj.Price50;
                firstOrDafaultProductFromDb.Price100 = obj.Price100;
                if (obj.ImageUrl != null)
                {
                    firstOrDafaultProductFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}